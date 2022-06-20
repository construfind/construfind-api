using Business.Models;
using Business.Utils;
using ConstruFindAPI.API.Configuration.Extensions;
using ConstruFindAPI.API.ViewModels;
using ConstruFindAPI.Data.Context;
using ConstruFindAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

namespace ConstruFindAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : BaseController
    {
        private readonly SignInManager<Usuario> _signinManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ConstrufindContext _dbContext;

        public ServicoController(SignInManager<Usuario> signinManager,
                                UserManager<Usuario> userManager,
                                IOptions<AppSettings> appSettings,
                                ConstrufindContext dbContext)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpPost("service-add")]
        public async Task<ActionResult> ServiceCreate([FromBody] ServicoViewModel servicoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {            
                var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

                if (user is null)
                {
                    ErrorProcess("Usuário inexistente com esse Email.");
                    return CustomResponse();
                }

                var servicoNovo = new Servico
                {
                    Descricao = servicoViewModel.Descricao,
                    Local = servicoViewModel.Local,
                    TipoServico = servicoViewModel.TipoServico,
                    Titulo = servicoViewModel.Titulo,
                    UsuarioContratanteForeignID = user.Documento,
                    UsuarioContratante = user
                };

                _dbContext.Add<Servico>(servicoNovo);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorProcess(ex.Message);
            }

            return CustomResponse("Serviço criado com Sucesso!");
        }

        [Authorize]
        [HttpGet("service-read")]
        public async Task<ActionResult> ServiceReadByID()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

                if (user is null)
                {
                    ErrorProcess("Usuário inexistente com esse Email.");
                    return CustomResponse();
                }

                var servicosExistentes = _dbContext.Servicos.ToList();

                if (servicosExistentes == null)
                {
                    ErrorProcess("Não existem serviços no momento, volte novamente em breve!");
                    return CustomResponse();
                }

                return CustomResponse(servicosExistentes);
            }
            catch (Exception ex)
            {
                ErrorProcess(ex.Message);
            }

            return CustomResponse("Erro interno ao buscar os serviços, contate o suporte.");
        }

        [Authorize]
        [HttpGet("service-read-user")]
        public async Task<ActionResult> ServiceReadByUser()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

                if (user is null)
                {
                    ErrorProcess("Usuário inexistente com esse Email.");
                    return CustomResponse();
                }

                var aux = _dbContext.Servicos.ToList();

                var servicosExistentes = _dbContext.Servicos.ToList().Where(x => x.UsuarioContratante.Email == user.Email);

                if (servicosExistentes == null)
                {
                    ErrorProcess("Não existem serviços no momento, crie serviços para buscar profissionais!");
                    return CustomResponse();
                }

                return CustomResponse(servicosExistentes);
            }
            catch (Exception ex)
            {
                ErrorProcess(ex.Message);
            }

            return CustomResponse("Erro interno ao buscar os serviços, contate o suporte.");
        }

        [Authorize]
        [HttpGet("service-read/{idServico}")]
        public async Task<ActionResult> ServiceReadByID([FromRoute] string idServico)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

                if (user is null)
                {
                    ErrorProcess("Usuário inexistente com esse Email.");
                    return CustomResponse();
                }

                var servicoExistente = _dbContext.Find<Servico>(Guid.Parse(idServico));

                if(servicoExistente == null)
                {
                    ErrorProcess("Serviço inexistente!");
                    return CustomResponse();
                }

                servicoExistente.UsuarioContratante = null;

                return CustomResponse(servicoExistente);
            }
            catch (Exception ex)
            {
                ErrorProcess(ex.Message);
            }

            return CustomResponse("Serviço criado com Sucesso!");
        }

        [Authorize]
        [HttpDelete("service-delete")]
        public async Task<ActionResult> ServiceDeleteByID([FromQuery] string idServico)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

                if (user is null)
                {
                    ErrorProcess("Usuário inexistente com esse Email.");
                    return CustomResponse();
                }

                var servicoExistente = _dbContext.Find<Servico>(Guid.Parse(idServico));

                if (servicoExistente == null)
                {
                    ErrorProcess("Serviço inexistente!");
                    return CustomResponse();
                }
                
                _dbContext.Remove(servicoExistente);
                _dbContext.SaveChanges();

                return CustomResponse("Serviço deletado com sucesso");
            }
            catch (Exception ex)
            {
                ErrorProcess(ex.Message);
            }

            return CustomResponse("Erro interno ao deletar o serviço, contate o suporte.");
        }

        [Authorize]
        [HttpPut("service-modify")]
        public async Task<ActionResult> ServiceModifyByID([FromQuery] string idServico, [FromBody] ServicoUpdateViewModel request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);

                if (user is null)
                {
                    ErrorProcess("Usuário inexistente com esse Email.");
                    return CustomResponse();
                }

                var servicoExistente = _dbContext.Find<Servico>(Guid.Parse(idServico));

                if (servicoExistente == null)
                {
                    ErrorProcess("Serviço inexistente!");
                    return CustomResponse();
                }

                servicoExistente.Titulo = request.Titulo;
                servicoExistente.Local = request.Local;
                servicoExistente.Descricao = request.Descricao;
                servicoExistente.TipoServico = request.TipoServico;

                _dbContext.Update(user);
                _dbContext.SaveChanges();

                return CustomResponse("Serviço alterado com Sucesso!");
            }
            catch (Exception ex)
            {
                ErrorProcess(ex.Message);
            }

            return CustomResponse("Erro interno ao alterar o serviço, contate o suporte.");
        }

        [HttpGet("service-types")]
        public ActionResult ServiceTypes([FromRoute] string cpf)
        {
            string[] response = {
                "Arquiteto",
                "Pedreiro",
                "Ajudante de Obra",
                "Armador",
                "Pintor",
                "Gesseiro",
                "Eletricista",
                "Encanador"
            };


            return CustomResponse(response);
        }
    }
}
