using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Data;

namespace TechStore.Controllers
{
    public class AuthController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = _context.Usuarios
                .FirstOrDefault(x => x.Email == model.Email);

            if (usuario == null)
            {
                ViewBag.Erro = "Usuário inválido";
                Console.WriteLine("Usuário não encontrado: " + model.Email);
                return View();
            }

            if (usuario.Senha != model.Senha)
            {
                ViewBag.Erro = "Senha inválida";
                Console.WriteLine("Senha incorreta para o usuário: " + model.Email);
                return View();
            }

            HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
            HttpContext.Session.SetString("Usuario", usuario.Nome);

            Console.WriteLine("Login realizado com sucesso para o usuário: " + model.Email);
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }
        public IActionResult Logout()
        {
            var usuario = HttpContext.Session.GetString("Usuario");

            Console.WriteLine($"Logout: {usuario}");

            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}