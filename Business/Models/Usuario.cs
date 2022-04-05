using Microsoft.AspNetCore.Identity;
using System;

namespace ConstruFindAPI.Business.Models
{
    public class Usuario : IdentityUser
    {
        public string Documento { get; set; }
        public string TipoUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimoAcesso { get; set; }
        public Endereco Endereco { get; set; }

    }
    public class UsuarioRole : IdentityRole
    {

    }
}