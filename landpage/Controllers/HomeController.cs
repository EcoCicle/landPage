using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;


namespace WebApplication1.Controllers;

// [Authorize]
public class HomeController : Controller{
    private readonly SupabaseService _supabaseService;

    public HomeController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    public async Task<IActionResult> BuscarConsumidores()
    {
        // Busca consumidores
        var content = await _supabaseService.GetConsumidoresAsync();
        var consumidores = JsonConvert.DeserializeObject<List<Consumidor>>(content) ?? new List<Consumidor>();
        return View("Index", consumidores);
    }

    public IActionResult Configuracao()
    {
        var consumidorJson = HttpContext.Session.GetString("Consumidor");

        if (consumidorJson != null)
        {
            var consumidor = JsonConvert.DeserializeObject<Consumidor>(consumidorJson);
            if (consumidor != null)
            {
                TempData["UserName"] = consumidor.name ?? "";
                TempData["UserEmail"] = consumidor.email ?? "";
            }
            else
            {
                TempData["UserName"] = "";
                TempData["UserEmail"] = "";
            }
        }

        return View("Configuracao");
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {

        await HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("LoginRegister", "Account");
    }
   
    public IActionResult Perfil()
    {
        return View("Perfil");
    }
    
    public IActionResult Produtos()
    {
        return View("Produtos");
    }
    
    public IActionResult Index()
    {
        return View();
    }
}