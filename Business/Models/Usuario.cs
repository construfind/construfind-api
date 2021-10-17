using ConstruFindAPI.Business.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace ConstruFindAPI.Business.Models
{
    public class Usuario : IdentityUser<string>
    {
        public string Documento { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimoAcesso { get; set; }
        public Endereco Endereco { get; set; }

    }
}