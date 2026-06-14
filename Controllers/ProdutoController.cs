using Microsoft.AspNetCore.Mvc;
using TechStore.Data;

namespace TechStore.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var produtos = _context.Produtos.ToList();

            return View(produtos);
        }
    }
}