using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class LoginRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClient(string email, string password)
        {
            // Valida, salva, redireciona
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult SubmitSeller(string email, string password, string storeName)
        {
            // Valida, salva, redireciona
            return RedirectToAction("Index", "Home");
        }
    }
}
