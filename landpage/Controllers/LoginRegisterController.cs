using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using WebApplication1.Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace WebApplication1.Controllers
{
    public class LoginRegisterController : Controller
    {
         private readonly SupabaseService _supabaseService;

        public LoginRegisterController(SupabaseService supabaseService)
        {
        
            _supabaseService = supabaseService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string emaillogin, string passwordlogin)
        {
            Console.WriteLine($"Email: {emaillogin}, Password: {passwordlogin}");
            var content = await _supabaseService.GetUser(emaillogin,passwordlogin);

            if (content == null)
            {
                TempData["LoginError"] = "Email ou senha inválidos."; 
                return RedirectToAction("LoginRegister", "Account");
            }
          
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, content.Name),
                new Claim(ClaimTypes.Email, content.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // cookie persistente
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(content));

            return RedirectToAction("Configuracao", "Home");
            
        }
    }
}
