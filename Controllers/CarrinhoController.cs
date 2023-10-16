using LaDoces2.Models;
using LaDoces2.Repositories.Interfaces;
using LaDoces2.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LaDoces2.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly Carrinho _carrinho;
        private readonly IItemRepository _itemRepository;
        public CarrinhoController(Carrinho carrinho, IItemRepository itemRepository)
        {
            _carrinho = carrinho;
            _itemRepository = itemRepository;
        } 
        public IActionResult Index()
        {
            var itens = _carrinho.GetCarrinhoCompraItems();
            _carrinho.CarrinhoItens = itens;
            var carrinhoVM = new CarrinhoViewModel
            {
                Carrinho = _carrinho,
                CarrinhoTotal = _carrinho.GetCarrinhoCompraTotal()
            };
            return View(carrinhoVM);
        }
        public IActionResult AdicionarItemNoCarrinhoCompra(int itemId)
        {
            var itemSelecionado = _itemRepository.Itens.FirstOrDefault(l => l.ItemId == itemId);
            if (itemSelecionado != null)
            {
                _carrinho.AdicionarItemCarrinho(itemSelecionado);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoverCarrinho(int itemId)
        {
            var itemSelecionado = _itemRepository.Itens.FirstOrDefault(l => l.ItemId == itemId);
            if (itemSelecionado != null)
            {
                _carrinho.RemoverItemDoCarrinhoCompra(itemSelecionado);
            }
            return RedirectToAction("Index");
        }
    }
}