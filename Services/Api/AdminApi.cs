using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class AdminApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public AdminApi(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<bool> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return false;

        _http.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PostAsJsonAsync("api/admin/employees", request);
        return response.IsSuccessStatusCode;
    }
}