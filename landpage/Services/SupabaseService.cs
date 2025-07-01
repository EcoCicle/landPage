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
}
