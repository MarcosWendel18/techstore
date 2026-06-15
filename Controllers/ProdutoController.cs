using Microsoft.AspNetCore.Mvc;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string pesquisa)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var produtos = _context.Produtos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = pesquisa.Trim();

                produtos = produtos.Where(p =>
                    p.Nome.ToLower().Contains(pesquisa) ||
                    p.Categoria.ToLower().Contains(pesquisa));
            }

            return View(produtos.ToList());
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return View(produto);
            }

            _context.Produtos.Add(produto);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(produto);
            }

            var produtoBanco = _context.Produtos.Find(id);

            if (produtoBanco == null)
            {
                return NotFound();
            }

            produtoBanco.Nome = produto.Nome;
            produtoBanco.Categoria = produto.Categoria;
            produtoBanco.Descricao = produto.Descricao;
            produtoBanco.Preco = produto.Preco;
            produtoBanco.Estoque = produto.Estoque;
            produtoBanco.Imagem = produto.Imagem;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}