using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models;

namespace TechStore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Sobre()
    {
        return View();
    }

    public IActionResult Produtos()
    {
        return View();
    }

    public IActionResult Contato()
    {
        return View();
    }
}
