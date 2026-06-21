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
                return View();
            }

            if (usuario.Senha != model.Senha)
            {
                ViewBag.Erro = "Senha inválida";
                return View();
            }

            HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
            HttpContext.Session.SetString("Usuario", usuario.Nome);

            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Logout()
        {
            var usuario = HttpContext.Session.GetString("Usuario");

            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}