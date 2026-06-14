using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        if(HttpContext.Session.GetString("Usuario") == null)
        {
            return RedirectToAction("Login","Auth");
        }

        return View();
    }
}