using System.Diagnostics;
using Dnw.WebShop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Dnw.WebShop.UI.Models;

namespace Dnw.WebShop.UI.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }
    
    public IActionResult Index()
    {
        var products = _productService.GetTopSellingProducts(5);
        return View(products);
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
