using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;


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
            Console.WriteLine($"email: {emaillogin}, Password: {passwordlogin}");
            var content = await _supabaseService.GetConsumidor(emaillogin, passwordlogin);

            if (content == null)
            {
                TempData["LoginError"] = "email ou senha inválidos.";
                return RedirectToAction("LoginRegister", "Account");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, content.name),
                new Claim(ClaimTypes.Email, content.email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // cookie persistente
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

            HttpContext.Session.SetString("Consumidor", JsonConvert.SerializeObject(content));

            return RedirectToAction("Configuracao", "Home");

        }
        [HttpPost]
public async Task<IActionResult> CreateUser(string emailconsumidor, string senhaconsumidor, string confirmarsenhaconsumidor)
{
    try
    {
        if (string.IsNullOrEmpty(emailconsumidor) || string.IsNullOrEmpty(senhaconsumidor) || string.IsNullOrEmpty(confirmarsenhaconsumidor))
        {
            return Json(new { error = true, message = "Todos os campos são obrigatórios." });
        }

        if (senhaconsumidor != confirmarsenhaconsumidor)
        {
            return Json(new { error = true, message = "As senhas não coincidem." });
        }

        var content = await _supabaseService.CreateConsumidor(emailconsumidor, senhaconsumidor);

        if (content == null)
        {
            return Json(new { error = true, message = "email já cadastrado." });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, content.name),
            new Claim(ClaimTypes.Email, content.email),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
        };

        await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

        HttpContext.Session.SetString("Consumidor", JsonConvert.SerializeObject(content));

        return Json(new { error = false, message = "Consumidor criado com sucesso." });
    }
    catch (Exception ex)
    {
        return Json(new { error = true, message = "Erro interno: " + ex.Message });
    }
}

[HttpPost]
public async Task<IActionResult> CreateVendedor(string emailvendedor, string senhavendedor, string confirmarsenhavendedor, string cnpjvendedor, string nomeloja, string descricaoloja)
{
    try
    {
        if (string.IsNullOrEmpty(emailvendedor) || string.IsNullOrEmpty(senhavendedor) || string.IsNullOrEmpty(confirmarsenhavendedor) || string.IsNullOrEmpty(cnpjvendedor) || string.IsNullOrEmpty(nomeloja) || string.IsNullOrEmpty(descricaoloja))
        {
            return Json(new { error = true, message = "Todos os campos são obrigatórios." });
        }

        if (senhavendedor != confirmarsenhavendedor)
        {
            return Json(new { error = true, message = "As senhas não coincidem." });
        }

        var vendedor = await _supabaseService.CreateVendedor(emailvendedor, senhavendedor, cnpjvendedor, nomeloja, descricaoloja);

        if (vendedor == null)
        {
            return Json(new { error = true, message = "Erro ao criar vendedor." });
        }

        bool lojaCriada = await _supabaseService.CreateLoja(vendedor.vendedor_id, nomeloja, cnpjvendedor, descricaoloja);

        if (!lojaCriada)
        {
            return Json(new { error = true, message = "Vendedor criado, mas erro ao criar loja." });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, vendedor.name),
            new Claim(ClaimTypes.Email, vendedor.email),
            new Claim("CNPJ", vendedor.cnpj),
            new Claim("Loja", vendedor.nomeloja)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
        };

        await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

        HttpContext.Session.SetString("User", JsonConvert.SerializeObject(vendedor));

        return Json(new { error = false, message = "Vendedor criado com sucesso." });
    }
    catch (Exception ex)
    {
        return Json(new { error = true, message = "Erro interno: " + ex.Message });
    }
}
    }
}
