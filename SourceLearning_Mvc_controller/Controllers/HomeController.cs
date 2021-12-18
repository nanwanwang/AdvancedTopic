using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SourceLearning_Mvc_controller.Models;

namespace SourceLearning_Mvc_controller.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [ServiceFilter(typeof(MyExceptionFilterAttribute))]
    public IActionResult Index()
    {
        throw new Exception("Home Index Error");
        return View();
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