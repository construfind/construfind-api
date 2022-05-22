using System;
using System.ComponentModel.DataAnnotations;

namespace ConstruFindAPI.Business.Models
{
    public class Endereco
    {
        [Key]
        public Guid codigoEndereco { get; set; } = Guid.NewGuid();
        public string numeroEndereco { get; set; }
        public string nomeLogradouro { get; set; }
        public string codigoCEP { get; set; }
        public string nomeBairro { get; set; }
        public string nomeCidade { get; set; }
        public string nomeEstado { get; set; }
        public string Sigla { get; set; }
    }
}