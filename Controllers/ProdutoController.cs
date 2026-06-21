using Microsoft.AspNetCore.Mvc;
using TechStore.Data;
using TechStore.Models;
using System.Globalization;

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
        public IActionResult Create(Produto produto, IFormFile? arquivoImagem)
        {

            var precoTexto = Request.Form["Preco"].ToString();

            if (!decimal.TryParse(
                    precoTexto,
                    NumberStyles.Any,
                    new CultureInfo("pt-BR"),
                    out decimal preco))
            {
                ModelState.AddModelError("Preco",
                    "Preço inválido.");

                return View(produto);
            }

            produto.Preco = preco;

            if (produto.Preco <= 0)
            {
                ModelState.AddModelError("Preco",
                    "O preço deve ser maior que zero.");

                return View(produto);
            }

            if (arquivoImagem != null)
            {
                var nomeArquivo =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(arquivoImagem.FileName);

                // 1. Defina o caminho da pasta de uploads
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // 2. Verifique se a pasta existe. Se não existir, crie-a.
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // 3. Combine o caminho da pasta com o nome do arquivo gerado
                var filePath = Path.Combine(uploadsFolder, nomeArquivo);


                using (var stream =
                    new FileStream(filePath, FileMode.Create))
                {
                    arquivoImagem.CopyTo(stream);
                }

                produto.Imagem =
                    "/uploads/" + nomeArquivo;
            }

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
        public IActionResult Edit(
    int id,
    Produto produto,
    IFormFile? arquivoImagem)
        {
            var precoTexto = Request.Form["Preco"].ToString();

            if (!decimal.TryParse(
                    precoTexto,
                    NumberStyles.Any,
                    new CultureInfo("pt-BR"),
                    out decimal preco))
            {
                ModelState.AddModelError("Preco",
                    "Preço inválido.");

                return View(produto);
            }

            produto.Preco = preco;

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

            if (arquivoImagem != null &&
    arquivoImagem.Length > 0)
            {
                var nome =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(
                        arquivoImagem.FileName);

                var pasta =
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "uploads");

                Directory.CreateDirectory(pasta);

                var caminho =
                    Path.Combine(pasta, nome);

                using var stream =
                    new FileStream(
                        caminho,
                        FileMode.Create);

                arquivoImagem.CopyTo(stream);

                produtoBanco.Imagem =
                    "/uploads/" + nome;
            }

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