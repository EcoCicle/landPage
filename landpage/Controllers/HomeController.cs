using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication1.Controllers;

[Authorize]
public class HomeController : Controller{
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
        var userJson = HttpContext.Session.GetString("User");

        if (userJson != null)
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);
            TempData["UserName"] = user.Name ; 
            TempData["UserEmail"] = user.Email ; 
        }

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




}