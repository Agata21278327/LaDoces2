using System.ComponentModel.DataAnnotations;

namespace LaDoces2.ViewModel
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "E-mail")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        [Required(ErrorMessage = "O nome deve ser informado")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A rua deve ser informado")]
        [Display(Name = "Rua")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "O numero deve ser informado")]
        [Display(Name = "Numero")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "O bairro deve ser informado")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "A cidade deve ser informado")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O estado deve ser informado")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "O CEP deve ser informado")]
        [Display(Name = "CEP")]
        public long Cep { get; set; }
    }
}