using LaDoces2.Models;
using LaDoces2.Repositories.Interfaces;
using LaDoces2.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LaDoces2.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRespository;
        public ItemController(IItemRepository itemRespository)
        {
            _itemRespository = itemRespository;
        }
        public IActionResult List(string categoria)
        {
            IEnumerable<Item> itens;
            var categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(categoria))
            {
                itens = _itemRespository.Itens.OrderBy(m => m.ItemId);
                categoriaAtual = "Todos os itens";
            }
            else
            {
                itens = _itemRespository.Itens.Where(m => m.Categoria.Nome.Equals(categoria)).OrderBy(m => m.ItemId);

                categoriaAtual = categoria;
            }
            var itemListViewModel = new ItemListViewModel
            {
                Itens = itens,
                CategoriaAtual = categoriaAtual
            };
            return View(itemListViewModel);
        }
        public IActionResult Details(int itemId)
        {
            var item = _itemRespository.Itens.FirstOrDefault(m => m.ItemId == itemId);

            return View(item);
        }
        public IActionResult Search(string searchString)
        {

            IEnumerable<Item> itens;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                itens = _itemRespository.Itens.OrderBy(m => m.Nome);
                categoriaAtual = "Todos os Itens";
            }
            else
            {
                itens = _itemRespository.Itens.Where(m => m.Nome.ToLower() == searchString.ToLower()).OrderBy(m => m.Nome);

                if (itens.Any())
                {
                    categoriaAtual = "Itens";
                }
                else
                {
                    categoriaAtual = "Nada encontrado";
                }
            }
            return View("~/Views/Item/List.cshtml", new ItemListViewModel
            {
                CategoriaAtual = categoriaAtual,
                Itens = itens
            });

        }
    }
}