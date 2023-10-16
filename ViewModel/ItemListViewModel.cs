using LaDoces2.Models;

namespace LaDoces2.ViewModel
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Itens { get; set; }
        public string CategoriaAtual { get; set; }
    }
}