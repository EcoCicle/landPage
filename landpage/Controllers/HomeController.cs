using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;


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
            TempData["UserName"] = user.name ?? ""; 
            TempData["UserEmail"] = user.email ?? ""; 
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