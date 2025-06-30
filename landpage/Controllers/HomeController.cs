using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace WebApplication1.Controllers;


public class HomeController : Controller
{
    string _supabaseUrl = string.Empty;
    string _supabaseApiKey = string.Empty;

    private readonly ILogger<HomeController> _logger;
   public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _supabaseUrl = configuration["Supabase:Url"] ?? throw new ArgumentNullException("Supabase:Url não configurado.");
        _supabaseApiKey = configuration["Supabase:ApiKey"] ?? throw new ArgumentNullException("Supabase:ApiKey não configurado.");

    }

    public async Task<IActionResult> BuscarUsuarios()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_supabaseUrl);

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("apikey", _supabaseApiKey); 
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _supabaseApiKey);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync("usuários");
        var content = await response.Content.ReadAsStringAsync();

       _logger.LogInformation("Conteúdo da resposta: {content}", content);
   
        var users = JsonConvert.DeserializeObject<List<User>>(content);
        if (users == null || users.Count == 0)
        {
            _logger.LogWarning("Nenhum usuário encontrado ou erro de desserialização.");
            return View("Index", new List<User>());
        }
        return View("Index",users);
    }

    public IActionResult Configuracao()
    {
        return View("Configuracao");
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
