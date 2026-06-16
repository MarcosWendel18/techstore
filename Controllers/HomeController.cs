using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var produtos = _context.Produtos
            .OrderByDescending(x => x.Id)
            .Take(3)
            .ToList();

        return View(produtos);
    }

    public IActionResult Sobre()
    {
        return View();
    }

    public IActionResult Produtos()
    {
        var produtos = _context.Produtos.ToList();

        return View(produtos);
    }

    public IActionResult Contato()
    {
        return View();
    }

}
