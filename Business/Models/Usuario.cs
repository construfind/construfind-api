using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Usuario : IdentityUser
    {
        [PersonalData]
        public string NomeCompleto { get; set; }

        [PersonalData]
        public string Documento { get; set; }

        [PersonalData]
        public string TipoUsuario { get; set; }

        [PersonalData]
        public DateTime DataCriacao { get; set; }

        [PersonalData]
        public DateTime DataUltimoAcesso { get; set; }

        [PersonalData]
        public string NumeroEndereco { get; set; }

        [PersonalData]
        public string NomeLogradouro { get; set; }

        [PersonalData]
        public string CodigoCEP { get; set; }

        [PersonalData]
        public string NomeBairro { get; set; }

        [PersonalData]
        public string NomeCidade { get; set; }

        [PersonalData]
        public string NomeEstado { get; set; }

        [PersonalData]
        public string SiglaEstado { get; set; }

        [PersonalData]
        public ICollection<Servico> Servicos { get; set; }

    }
    public class UsuarioRole : IdentityRole
    {

    }
}