using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;

namespace OfficeCore.Client.Services.Api;

public class SettingsApi
{
    private readonly HttpClient _http;

    public SettingsApi(HttpClient http)
    {
        _http = http;
    }

    // ===== DEPARTMENTS =====
    
    public async Task<List<DepartmentDto>> GetDepartmentsAsync()
    {
        try
        {
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
            var response = await _http.PostAsJsonAsync("api/settings/departments", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateDepartmentAsync(Guid id, UpdateDepartmentRequest request)
    {
        try
        {
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
            var response = await _http.DeleteAsync($"api/settings/roles/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
