using System.ComponentModel.DataAnnotations;

namespace LaDoces2.Models
{
    public class Categoria
    {
        public int CategoriaId{get;set;}
        [Display(Name ="Nome da Categoria")]
        public string Nome {get;set;}

    }
}