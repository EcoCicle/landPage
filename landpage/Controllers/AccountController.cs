using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LoginRegister()
        {
            return View();
        }
        public IActionResult Produtos()
        {
            return View("Produtos");
        }
        public IActionResult EsqueceuSenha()
        {
            return View("EsqueceuSenha");
        }
    }
}