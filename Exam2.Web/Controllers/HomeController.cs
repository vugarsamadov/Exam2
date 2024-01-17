using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Exam2.Web.Models;
using Exam2.Business.Services.Abstract;
using Exam2.Business.Models;

namespace Exam2.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAboutItemService aboutItemService;

    public HomeController(ILogger<HomeController> logger,IAboutItemService aboutItemService)
    {
        _logger = logger;
        this.aboutItemService = aboutItemService;
    }

    public async Task<IActionResult> Index()
    {
        var items = await aboutItemService.GetLastNAsync(3);
        var model = new AboutItemIndexVM()
        {
            AboutItems = items
        };
        return View(model);
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
