using System.Net.Http.Json;
using System.Net.Http.Headers;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class SettingsApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public SettingsApi(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    private void SetAuthHeader()
    {
        if (!string.IsNullOrWhiteSpace(_auth.AccessToken))
        {
            _http.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _auth.AccessToken);
        }
    }

    // ===== DEPARTMENTS =====
    
    public async Task<List<DepartmentDto>> GetDepartmentsAsync()
    {
        try
        {
            SetAuthHeader();
            var result = await _http.GetFromJsonAsync<List<DepartmentDto>>("api/settings/departments");
            return result ?? new List<DepartmentDto>();
        }
        catch
        {
            return new List<DepartmentDto>();
        }
    }

    public async Task<bool> CreateDepartmentAsync(CreateDepartmentRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PostAsJsonAsync("api/settings/departments", request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Create department failed: {response.StatusCode} - {error}");
            }
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception creating department: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> UpdateDepartmentAsync(Guid id, UpdateDepartmentRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PutAsJsonAsync($"api/settings/departments/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteDepartmentAsync(Guid id)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.DeleteAsync($"api/settings/departments/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ===== POSITIONS =====
    
    public async Task<List<PositionDto>> GetPositionsAsync()
    {
        try
        {
            SetAuthHeader();
            var result = await _http.GetFromJsonAsync<List<PositionDto>>("api/settings/positions");
            return result ?? new List<PositionDto>();
        }
        catch
        {
            return new List<PositionDto>();
        }
    }

    public async Task<bool> CreatePositionAsync(CreatePositionRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PostAsJsonAsync("api/settings/positions", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdatePositionAsync(Guid id, UpdatePositionRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PutAsJsonAsync($"api/settings/positions/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeletePositionAsync(Guid id)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.DeleteAsync($"api/settings/positions/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ===== ROLES =====
    
    public async Task<List<RoleDto>> GetRolesAsync()
    {
        try
        {
            SetAuthHeader();
            var result = await _http.GetFromJsonAsync<List<RoleDto>>("api/settings/roles");
            return result ?? new List<RoleDto>();
        }
        catch
        {
            return new List<RoleDto>();
        }
    }

    public async Task<bool> CreateRoleAsync(CreateRoleRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PostAsJsonAsync("api/settings/roles", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateRoleAsync(Guid id, UpdateRoleRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PutAsJsonAsync($"api/settings/roles/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteRoleAsync(Guid id)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.DeleteAsync($"api/settings/roles/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ===== CLIENTS =====
    
    public async Task<List<ClientDto>> GetClientsAsync()
    {
        try
        {
            SetAuthHeader();
            var result = await _http.GetFromJsonAsync<List<ClientDto>>("api/settings/clients");
            return result ?? new List<ClientDto>();
        }
        catch
        {
            return new List<ClientDto>();
        }
    }

    public async Task<bool> CreateClientAsync(CreateClientRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PostAsJsonAsync("api/settings/clients", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateClientAsync(Guid id, UpdateClientRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PutAsJsonAsync($"api/settings/clients/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteClientAsync(Guid id)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.DeleteAsync($"api/settings/clients/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // ===== BUSINESSES =====
    
    public async Task<List<BusinessDto>> GetBusinessesAsync()
    {
        try
        {
            SetAuthHeader();
            var result = await _http.GetFromJsonAsync<List<BusinessDto>>("api/settings/businesses");
            return result ?? new List<BusinessDto>();
        }
        catch
        {
            return new List<BusinessDto>();
        }
    }

    public async Task<bool> CreateBusinessAsync(CreateBusinessRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PostAsJsonAsync("api/settings/businesses", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateBusinessAsync(Guid id, UpdateBusinessRequest request)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.PutAsJsonAsync($"api/settings/businesses/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteBusinessAsync(Guid id)
    {
        try
        {
            SetAuthHeader();
            var response = await _http.DeleteAsync($"api/settings/businesses/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
