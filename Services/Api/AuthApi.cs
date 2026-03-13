using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class AuthApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public AuthApi(HttpClient http, AuthState authState) 
        => (_http, _auth) = (http, authState);

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);
        if (!response.IsSuccessStatusCode) 
            return null;
            
        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<UserProfileDto?> GetMyProfileAsync()
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync("api/auth/me");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserProfileDto>();
    }

    public async Task<ChangePasswordResponse?> ChangePasswordAsync(ChangePasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PostAsJsonAsync("api/auth/change-password", request);
        if (!response.IsSuccessStatusCode) 
            return null;

        return await response.Content.ReadFromJsonAsync<ChangePasswordResponse>();
    }
}