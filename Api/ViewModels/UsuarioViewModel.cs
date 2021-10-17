using ConstruFindAPI.API.ViewModels.CustomValidators;
using ConstruFindAPI.Business.Enums;
using ConstruFindAPI.Business.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConstruFindAPI.API.ViewModels
{

    public class UsuarioCreate
    {

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        [CPFCNPJCustomValidation(ErrorMessage = "O campo {0} est� em um formato inv�lido")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "O campo {0} est� em um formato inv�lido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        public TipoUsuario TipoUsuario { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        public Endereco Endereco { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas n�o est�o iguais.")]
        public string SenhaConfirmacao { get; set; }
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "O campo {0} est� em um formato inv�lido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
        public string Senha { get; set; }
    }

    public class UserLoginReturn
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
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
}