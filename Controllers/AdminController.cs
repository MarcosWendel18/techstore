using Microsoft.AspNetCore.Mvc;
using TechStore.Data;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Usuario") == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        ViewBag.TotalProdutos = _context.Produtos.Count();

        ViewBag.TotalCategorias = _context.Produtos
            .Select(p => p.Categoria)
            .Distinct()
            .Count();

        ViewBag.UltimosProdutos = _context.Produtos
            .OrderByDescending(p => p.Id)
            .Take(5)
            .ToList();

        return View();
    }
}