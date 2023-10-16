using System.ComponentModel.DataAnnotations;

namespace LaDoces2.Models
{
    public class Item
    {
        public int ItemId {get;set;}
        [Display(Name ="Nome do Item")]
        [Required(ErrorMessage ="Informe o nome do item")]
        [MinLength(4, ErrorMessage ="Nome deve ter no minimo {1} caracteres")]
        [MaxLength(30, ErrorMessage ="Nome dever ter no maximo {1} caracteres")]
        public string Nome{get;set;}
        [Display(Name ="Descrição Curta")]
        [Required(ErrorMessage ="Informe a Descrição Curta")]
        [MinLength(4, ErrorMessage ="Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(50, ErrorMessage ="Descrição dever ter no maximo {1} caracteres")]
        public string DescricaoCurta{get;set;}
        [Display(Name ="Descrição Detalhada")]
        [Required(ErrorMessage ="Informe a Descrição Detalhada")]
        [MinLength(20, ErrorMessage ="Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage ="Descrição dever ter no maximo {1} caracteres")]
        public string DescricaoDetalhada{get;set;}
        [Display(Name ="Preço")]
        [Required(ErrorMessage ="Informe o preço")]
        public double Preco{get; set;}
        [Display(Name ="Imagem Pequena")]
       
        public string ImagemPequenaUrl{get;set;}
        [Display(Name ="Imagem Normal")]
        public string ImagemUrl{get; set;}
        public bool Ativo{get;set;}
        public bool Destaque{get;set;}
        [Display(Name ="Categoria")]
        public int CategoriaId{get;set;}
        public virtual Categoria Categoria{get;set;}
    }
}