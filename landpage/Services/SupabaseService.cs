using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
public class SupabaseService
{
    private readonly HttpClient _client;

    public SupabaseService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _client = httpClientFactory.CreateClient();
        _client.BaseAddress = new Uri(configuration["Supabase:Url"]);
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("apikey", configuration["Supabase:ApiKey"]);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["Supabase:ApiKey"]);
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetUsersAsync()
    {
        var response = await _client.GetAsync("usuários");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    public async Task<User?> GetUser(string email, string password)
    {
        var response = await _client.GetAsync($"usuários?email=eq.{Uri.EscapeDataString(email)}&senha=eq.{Uri.EscapeDataString(password)}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<List<User>>(content);

        return users?.FirstOrDefault();
    }
    public async Task<User?> CreateUser(string email, string password)
    {
        var user = new User
        {
            name = "Consumidor",
            email = email,
            senha = password,
            image = "",
            data_criacao = DateTime.UtcNow,
        };
        var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
        content.Headers.Add("Prefer", "return=representation");
        var response = await _client.PostAsync("usuários", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Email: reponse: {response.StatusCode}, Content: {await response.Content.ReadAsStringAsync()}");

        if (response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(responseContent);
               return users?.FirstOrDefault();
        }

        return null;
      }
      public async Task<User?> CreateVendedor(string email, string senha, string nomeloja, string descricaoloja)
{
    // Cria o usuário vendedor
    var user = new User
    {
        name = "Vendedor",
        email = email,
        senha = senha,
        image = "",
        data_criacao = DateTime.UtcNow
    };
    var content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");
    content.Headers.Add("Prefer", "return=representation");
    var response = await _client.PostAsync("usuarios", content);
    var responseContent = await response.Content.ReadAsStringAsync();

    if (response.StatusCode == System.Net.HttpStatusCode.Created)
    {
        var users = JsonConvert.DeserializeObject<List<User>>(responseContent);
        var vendedor = users?.FirstOrDefault();
        if (vendedor != null)
        {
            // Cria a loja vinculada ao vendedor
            var loja = new {
                nomeloja = nomeloja,
                descricaoloja = descricaoloja,
                emailvendedor = email,
                data_criacao = DateTime.UtcNow
            };
            var lojaContent = new StringContent(JsonConvert.SerializeObject(loja), System.Text.Encoding.UTF8, "application/json");
            lojaContent.Headers.Add("Prefer", "return=representation");
            var lojaResponse = await _client.PostAsync("lojas", lojaContent);
            // Você pode tratar a resposta da loja se quiser
            return new User {
                name = vendedor.name,
                email = vendedor.email,
                senha = vendedor.senha,
                image = vendedor.image,
                data_criacao = vendedor.data_criacao
            };
        }
    }
    return null;
}
}
