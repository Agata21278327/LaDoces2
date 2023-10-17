using System.ComponentModel.DataAnnotations;

namespace LaDoces2.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "E-mail")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}