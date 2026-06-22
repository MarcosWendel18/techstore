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

    public IActionResult Produtos(string busca)
    {
        var produtos = _context.Produtos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca?.ToLower()))
        {
            produtos = produtos.Where(p =>
                p.Nome.ToLower().Contains(busca.ToLower()));
        }

        var lista = produtos.ToList();

        ViewBag.TotalProdutos = lista.Count;

        return View(lista);
    }

    public IActionResult Contato()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Contato(string nome,
                             string email,
                             string mensagem)
    {
        TempData["Sucesso"] =
            "Sua mensagem foi enviada com sucesso. Agradecemos seu contato!";

        return RedirectToAction(nameof(Contato));
    }

}
