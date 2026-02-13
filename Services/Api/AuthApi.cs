using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;

namespace OfficeCore.Client.Services.Api;

public class AuthApi
{
    private readonly HttpClient _http;

    public AuthApi(HttpClient http) => _http = http;

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        if (!response.IsSuccessStatusCode) 
            return null;
            
        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }
}