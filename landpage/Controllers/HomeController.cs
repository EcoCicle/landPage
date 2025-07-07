using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace WebApplication1.Controllers;

// [Authorize]
public class HomeController : Controller
{
    private readonly SupabaseService _supabaseService;

    public HomeController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    public async Task<IActionResult> BuscarConsumidores()
    {
        var content = await _supabaseService.GetConsumidoresAsync();
        var consumidores = JsonConvert.DeserializeObject<List<Consumidor>>(content) ?? new List<Consumidor>();
        return View("Index", consumidores);
    }

    private IActionResult CarregarPerfilView()
    {
        var consumidorJson = HttpContext.Session.GetString("Consumidor");

        if (string.IsNullOrEmpty(consumidorJson))
        {
            return RedirectToAction("LoginRegister", "Account");
        }

        var consumidor = JsonConvert.DeserializeObject<Consumidor>(consumidorJson);

        if (consumidor == null || consumidor.name == null)
        {
            return RedirectToAction("LoginRegister", "Account");
        }

        var nomeParts = consumidor.name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var firstName = nomeParts.Length > 0 ? nomeParts[0] : "";
        var lastName = nomeParts.Length > 1 ? string.Join(" ", nomeParts.Skip(1)) : "";

        ViewBag.FirstName = firstName;
        ViewBag.LastName = lastName;
        ViewBag.Email = consumidor.email;

        return View("Perfil");
    }

    // AÇÕES QUE USAM O MÉTODO REUTILIZÁVEL
    public IActionResult Perfil()
    {
        return CarregarPerfilView();
    }

    [HttpGet("Home/Configuracao/Perfil")]
    public IActionResult PerfilFromConfig()
    {
        return CarregarPerfilView();
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePerfil(string firstName, string lastName)
    {
        var consumidorJson = HttpContext.Session.GetString("Consumidor");

        if (string.IsNullOrEmpty(consumidorJson))
        {
            TempData["ErrorMessage"] = "Sessão inválida";
            return RedirectToAction("PerfilFromConfig"); // Alterado para rota hierárquica
        }

        var consumidor = JsonConvert.DeserializeObject<Consumidor>(consumidorJson);

        if (consumidor == null)
        {
            TempData["ErrorMessage"] = "Dados corrompidos";
            return RedirectToAction("PerfilFromConfig"); // Alterado para rota hierárquica
        }

        // VALIDAÇÃO SIMPLIFICADA
        if (string.IsNullOrWhiteSpace(firstName))
        {
            TempData["ErrorMessage"] = "Nome inválido";
            return RedirectToAction("PerfilFromConfig"); // Alterado para rota hierárquica
        }

        // Atualização do nome
        consumidor.name = $"{firstName.Trim()} {lastName?.Trim()}".Trim();

        var success = await _supabaseService.UpdateConsumidor(consumidor);

        if (success)
        {
            HttpContext.Session.SetString("Consumidor", JsonConvert.SerializeObject(consumidor));
            TempData["SuccessMessage"] = "Perfil atualizado com sucesso!";
        }
        else
        {
            TempData["ErrorMessage"] = "Erro ao atualizar perfil";
        }

        return RedirectToAction("PerfilFromConfig"); // Alterado para rota hierárquica
    }


    public IActionResult Configuracao()
    {
        var consumidorJson = HttpContext.Session.GetString("Consumidor");

        if (string.IsNullOrEmpty(consumidorJson))
        {
            TempData["UserName"] = "";
            TempData["UserEmail"] = "";
            return View("Configuracao");
        }

        var consumidor = JsonConvert.DeserializeObject<Consumidor>(consumidorJson);

        TempData["UserName"] = consumidor?.name ?? "";
        TempData["UserEmail"] = consumidor?.email ?? "";

        return View("Configuracao");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("LoginRegister", "Account");
    }

    public IActionResult Produtos()
    {
        return View("Produtos");
    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> DeleteAccount()
    {
        var consumidorJson = HttpContext.Session.GetString("Consumidor");
        if (string.IsNullOrEmpty(consumidorJson))
        {
            return RedirectToAction("LoginRegister", "Account");
        }

        var consumidor = JsonConvert.DeserializeObject<Consumidor>(consumidorJson);
        if (consumidor == null)
        {
            return RedirectToAction("LoginRegister", "Account");
        }

        var success = await _supabaseService.DeleteConsumidor(consumidor.id);

        if (success)
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["ErrorMessage"] = "Erro ao excluir sua conta. Tente novamente.";
            return RedirectToAction("Perfil");
        }
    }
}