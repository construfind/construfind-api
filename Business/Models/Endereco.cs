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
        public Bairro bairroEndereco { get; set; }
    }

    public class Bairro
    {
        [Key]
        public Guid codigoBairro { get; set; } = Guid.NewGuid();
        public string nomeBairro { get; set; }
        public Cidade cidadeBairro { get; set; }
    }

    public class Cidade
    {
        [Key]
        public Guid codigoCidade { get; set; } = Guid.NewGuid();
        public string nomeCidade { get; set; }
        public Estado estadoCidade { get; set; }
    }

    public class Estado
    {
        [Key]
        public Guid codigoEstado { get; set; } = Guid.NewGuid();
        public string nomeEstado { get; set; }
        public string Sigla { get; set; }
    }
}