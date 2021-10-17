using ConstruFindAPI.API.Configuration.Extensions;
using ConstruFindAPI.API.ViewModels;
using ConstruFindAPI.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConstruFindAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly SignInManager<Usuario> _signinManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly AppSettings _appSettings;

        public UsuarioController(SignInManager<Usuario> signinManager,
                                UserManager<Usuario> userManager,
                                IOptions<AppSettings> appSettings)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("user-register")]
        public async Task<ActionResult> Register(UsuarioCreate userCreateModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new Usuario
            {
                UserName = userCreateModel.Email,
                Email = userCreateModel.Email,
                PhoneNumber = userCreateModel.Telefone,
                Documento = userCreateModel.Documento,
                DataCriacao = DateTime.Now,
                DataUltimoAcesso = DateTime.Now,
                Endereco = userCreateModel.Endereco,
                TipoUsuario = userCreateModel.TipoUsuario,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false
            };

            var res = await _userManager.CreateAsync(user, userCreateModel.Senha);

            if (res.Succeeded)
            {
                return CustomResponse(await GenerateJWT(userCreateModel.Email));
            }

            foreach (var error in res.Errors)
            {
                ErrorProcess(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("user-auth")]
        public async Task<ActionResult> Login(UserLogin userLoginModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var res = await _signinManager.PasswordSignInAsync(userLoginModel.Email, userLoginModel.Senha, false, true);

            if (res.Succeeded)
            {
                return CustomResponse(await GenerateJWT(userLoginModel.Email));
            }

            if (res.IsLockedOut)
            {
                ErrorProcess("Usuário temporariamente bloqueado por excesso de tentativas inválidas.");
                return CustomResponse();
            }

            ErrorProcess("Usuário ou senha incorretos.");
            return CustomResponse();
        }

        private async Task<UserLoginReturn> GenerateJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GenerateClaims(userClaims, user);
            var encodedToken = TokenEncode(identityClaims);

            return GetToken(encodedToken, user, userClaims);
        }

        private async Task<ClaimsIdentity> GenerateClaims(ICollection<Claim> userClaims, Usuario user)
        {
            //Claims
            var userRoles = await _userManager.GetRolesAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in userRoles)
            {
                userClaims.Add(new Claim("role", role));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);
            return identityClaims;
        }

        private string TokenEncode(ClaimsIdentity claims)
        {
            //Create Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValideOn,
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            //Return token
            return tokenHandler.WriteToken(token);

        }

        private UserLoginReturn GetToken(string encodedToken, Usuario user, IEnumerable<Claim> userClaims)
        {
            return new UserLoginReturn
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.Expires).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = userClaims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
