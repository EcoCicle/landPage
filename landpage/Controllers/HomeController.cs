using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly SupabaseService _supabaseService;

    public HomeController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    public async Task<IActionResult> BuscarUsuarios()
    {
        var content = await _supabaseService.GetUsersAsync();
        var users = JsonConvert.DeserializeObject<List<User>>(content) ?? new List<User>();
        return View("Index", users);
    }

    public IActionResult Configuracao()
    {
        return View("Configuracao");
    }
    
    [HttpGet("Home/Configuracao/Perfil")]
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
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}