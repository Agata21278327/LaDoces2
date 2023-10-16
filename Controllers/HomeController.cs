using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LaDoces2.Models;
using LaDoces2.Repositories.Interfaces;
using LaDoces2.ViewModel;

namespace LaDoces2.Controllers;

public class HomeController : Controller
{
    private readonly IItemRepository _itemRepository;
    public HomeController(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel
        {
            ItensEmDestaque = _itemRepository.ItensEmDestaque
        };
        return View(homeViewModel);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
