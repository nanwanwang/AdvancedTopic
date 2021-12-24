using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using SourceLearning_Mvc_controller.Models;

namespace SourceLearning_Mvc_controller.Controllers;
[MyOrderActionFilterAttribute2]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    [MiddlewareFilter(typeof(MyPiple))]
    [MyActionFilterWithController]
    [ServiceFilter(typeof(MyExceptionFilterAttribute))]
    [ServiceFilter(typeof(MyActionFilterWithSeriverFilter),Order = 2)]
    [TypeFilter(typeof(MyActionFilterWithTypeFilter),Arguments = new object[]{"Index"},Order = 1)]
    public IActionResult Index()
    {
       // throw new Exception("Home Index Error");
        return View();
    }
    
    
    [MyOrderActionFilterAttribute1]
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