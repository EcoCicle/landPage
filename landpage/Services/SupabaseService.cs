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

    public async Task<Consumidor?> CreateConsumidor(string email, string senha)
{
    var consumidor = new Consumidor
    {
        name = "Consumidor",
        email = email,
        senha = senha,
        image = "",
        data_criacao = DateTime.UtcNow
    };
    var content = new StringContent(JsonConvert.SerializeObject(consumidor), System.Text.Encoding.UTF8, "application/json");
    content.Headers.Add("Prefer", "return=representation");
    var response = await _client.PostAsync("consumidores", content);
    var responseContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Status: {response.StatusCode}, Content: {responseContent}");

    if (response.StatusCode == System.Net.HttpStatusCode.Created)
    {
        var consumidores = JsonConvert.DeserializeObject<List<Consumidor>>(responseContent);
        return consumidores?.FirstOrDefault();
    }
    return null;
}

public async Task<Vendedor?> CreateVendedor(string email, string senha, string cnpj, string nomeloja, string descricaoloja)
{
    var vendedor = new Vendedor
    {
        name = "Vendedor",
        email = email,
        senha = senha,
        image = "",
        data_criacao = DateTime.UtcNow,
        cnpj = cnpj,
        nomeloja = nomeloja,
        descricaoloja = descricaoloja
    };
    var content = new StringContent(JsonConvert.SerializeObject(vendedor), System.Text.Encoding.UTF8, "application/json");
    content.Headers.Add("Prefer", "return=representation");
    var response = await _client.PostAsync("vendedores", content);
    var responseContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Status: {response.StatusCode}, Content: {responseContent}");

    if (response.StatusCode == System.Net.HttpStatusCode.Created)
    {
        var vendedores = JsonConvert.DeserializeObject<List<Vendedor>>(responseContent);
        return vendedores?.FirstOrDefault();
    }
    return null;
}

public async Task<Consumidor?> GetConsumidor(string email, string senha)
{
    var response = await _client.GetAsync($"consumidores?email=eq.{Uri.EscapeDataString(email)}&senha=eq.{Uri.EscapeDataString(senha)}");
    response.EnsureSuccessStatusCode();
    var content = await response.Content.ReadAsStringAsync();
    var consumidores = JsonConvert.DeserializeObject<List<Consumidor>>(content);
    return consumidores?.FirstOrDefault();
}

public async Task<string> GetConsumidoresAsync()
{
    var response = await _client.GetAsync("consumidores");
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadAsStringAsync();
}
}
