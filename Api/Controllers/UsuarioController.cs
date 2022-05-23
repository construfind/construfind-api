using Business.Utils;
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
        public async Task<ActionResult> Register([FromBody] UsuarioCreate userCreateModel)
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
                Endereco = new Endereco
                {
                    numeroEndereco = userCreateModel.Endereco.Numero,
                    nomeLogradouro = userCreateModel.Endereco.Rua,
                    codigoCEP = userCreateModel.Endereco.CEP,
                    nomeBairro = userCreateModel.Endereco.Bairro,
                    nomeCidade = userCreateModel.Endereco.Bairro,
                    nomeEstado = EnderecoUtils.GetEstadoSigla(userCreateModel.Endereco.UF),
                    Sigla = userCreateModel.Endereco.UF                
                },
                TipoUsuario = userCreateModel.TipoUsuario,
                EmailConfirmed = true,
            };

            var res = await _userManager.CreateAsync(user, userCreateModel.Senha);

            if (res.Succeeded)
            {
                return CustomResponse(await GenerateJWT(user));
            }

            foreach (var error in res.Errors)
            {
                ErrorProcess(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("user-auth")]
        public async Task<ActionResult> Login([FromBody] UserLogin userLoginModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = _userManager.Users.SingleOrDefault(x => x.Documento == userLoginModel.CPF);

            if(user == null)
            {
                ErrorProcess("Usuário ou senha incorretos.");
                return CustomResponse();
            }

            var res = await _signinManager.PasswordSignInAsync(user, userLoginModel.Senha, false, true);

            if (res.Succeeded)
            {
                return CustomResponse(await GenerateJWT(user));
            }

            if (res.IsLockedOut)
            {
                ErrorProcess("Usuário temporariamente bloqueado por excesso de tentativas inválidas.");
                return CustomResponse();
            }

            ErrorProcess("Usuário ou senha incorretos.");
            return CustomResponse();
        }

        [HttpGet("user-read/{cpf}")]
        public ActionResult ReadByCPF([FromRoute] string cpf)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = _userManager.Users.SingleOrDefault(x => x.Documento == cpf);

            if (user == null)
            {
                ErrorProcess("Usuário inexistente.");
                return CustomResponse();
            }
            
            var response = new UserReadViewModel
            {
                AccessFailedCount = user.AccessFailedCount,
                DataCriacao = user.DataCriacao,
                DataUltimoAcesso = user.DataUltimoAcesso,
                Documento = user.Documento,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnd = user.LockoutEnd,
                LockoutEnabled = user.LockoutEnabled,
                Endereco = user.Endereco,
                Id = user.Id,
                NormalizedEmail = user.NormalizedEmail,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                NormalizedUserName = user.NormalizedUserName,
                TipoUsuario = user.TipoUsuario,
                UserName = user.UserName
            };

            return CustomResponse(response);
        }

        [HttpPut("user-modify")]
        public async Task<ActionResult> Modify([FromQuery] string CPF, UserModifyDTO userModifyDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = _userManager.Users.SingleOrDefault(x => x.Documento == CPF);

            if (user == null)
            {
                ErrorProcess("Usuário inexistente.");
                return CustomResponse();
            }

            user.Endereco.numeroEndereco = userModifyDTO.Endereco.Numero;
            user.Endereco.nomeLogradouro = userModifyDTO.Endereco.Rua;
            user.Endereco.codigoCEP = userModifyDTO.Endereco.CEP;
            user.Endereco.nomeBairro = userModifyDTO.Endereco.Bairro;
            user.Endereco.nomeCidade = userModifyDTO.Endereco.Cidade;
            user.Endereco.nomeEstado = EnderecoUtils.GetEstadoSigla(userModifyDTO.Endereco.UF);
            user.Endereco.Sigla = userModifyDTO.Endereco.UF;

            user.PhoneNumber = userModifyDTO.Telefone;

            var res = await _userManager.UpdateAsync(user);

            if (res.Succeeded)
            {
                return CustomResponse(res);
            }

            ErrorProcess("Usuário inexistente.");
            return CustomResponse();
        }

        [HttpDelete("user-delete")]
        public async Task<ActionResult> Delete([FromQuery] string CPF)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = _userManager.Users.SingleOrDefault(x => x.Documento == CPF);

            if (user == null)
            {
                ErrorProcess("Usuário inexistente.");
                return CustomResponse();
            }

            var res = await _userManager.DeleteAsync(user);

            if (res.Succeeded)
            {
                return CustomResponse(res);
            }

            ErrorProcess("Usuário inexistente.");
            return CustomResponse();
        }

        private async Task<UserLoginReturn> GenerateJWT(Usuario user)
        {
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
                },
                UserInfo = new UserInfo
                {
                    CPF = user.Documento,
                    Nome = user.UserName,
                    TipoUsuario = user.TipoUsuario,
                    Email = user.Email,
                    Endereco = user.Endereco,
                    Telefone = user.PhoneNumber                    
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
