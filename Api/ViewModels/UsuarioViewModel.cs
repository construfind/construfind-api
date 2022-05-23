using ConstruFindAPI.API.ViewModels.CustomValidators;
using ConstruFindAPI.Business.Models;
using ConstruFindAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConstruFindAPI.API.ViewModels
{

    public class UsuarioCreate
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [CPFCNPJCustomValidation(ErrorMessage = "O campo {0} está em um formato inválido")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string TipoUsuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public EnderecoCreate Endereco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não estão iguais.")]
        public string SenhaConfirmacao { get; set; }
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [CPFCNPJCustomValidation(ErrorMessage = "O campo {0} está em um formato inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Senha { get; set; }
    }

    public class UserLoginReturn
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
        public UserInfo UserInfo { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class UserInfo
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string TipoUsuario { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoViewModel Endereco { get; set; }
    }

    public class UserReadViewModel
    {
        public string NomeCompleto { get; set; }
        public string Documento { get; set; }
        public string TipoUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimoAcesso { get; set; }        
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string NumeroEndereco { get; set; }
        public string NomeLogradouro { get; set; }
        public string CodigoCEP { get; set; }
        public string NomeBairro { get; set; }
        public string NomeCidade { get; set; }
        public string NomeEstado { get; set; }
        public string SiglaEstado { get; set; }
    }

    public class UserModifyDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [CPFCNPJCustomValidation(ErrorMessage = "O campo {0} está em um formato inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public EnderecoCreate Endereco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Telefone { get; set; }
    }
}