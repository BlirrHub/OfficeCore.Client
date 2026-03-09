using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;
using OfficeCore.Client.Features.Employees;

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

        var response = await _http.PostAsJsonAsync("api/admin/create-employee", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<Employee>?> GetAllEmployeesAsync()
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync("api/admin/employees");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Employee>>();
        }
        
        return null;
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/employees/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<Employee>();
    }

    public async Task<bool> UpdateEmployeeAsync(Guid id, Employee employee)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return false;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var payload = new
        {
            employee.FirstName,
            employee.LastName,
            employee.Department,
            employee.Position,
            employee.BiometricId,
            employee.DailyRate,
            employee.SchedTimeIn,
            employee.SchedTimeOut,
            employee.IsOTEnabled,
            employee.HasSSSDeduction,
            employee.HasPhilHealthDeduction,
            employee.HasPagIBIGDeduction,
            employee.IsActive,
            employee.Roles
        };
                // StdHoursPerDay removed

        var response = await _http.PutAsJsonAsync($"api/admin/employees/{id}", payload);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return false;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.DeleteAsync($"api/admin/employees/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<WeeklyAttendanceResponse?> GetWeeklyAttendanceAsync(DateOnly weekStart)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/attendance/weekly?weekStart={weekStart:yyyy-MM-dd}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<WeeklyAttendanceResponse>();
    }

    public async Task<ImportAttendanceResponse?> ImportAttendanceAsync(Stream fileStream, string fileName)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        using var content = new MultipartFormDataContent();
        using var streamContent = new StreamContent(fileStream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        content.Add(streamContent, "file", fileName);

        var response = await _http.PostAsync("api/admin/attendance/import", content);
        if (!response.IsSuccessStatusCode)
        {
            // Try to read error message from response
            try
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(errorContent))
                {
                    var errorObj = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(
                        errorContent, 
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    if (errorObj?.ContainsKey("message") == true)
                    {
                        var message = errorObj["message"].GetString() ?? "Import failed";
                        throw new Exception(message);
                    }
                }
            }
            catch (System.Text.Json.JsonException)
            {
                // Ignore JSON parsing errors, fall through to return null
            }
            
            return null;
        }

        return await response.Content.ReadFromJsonAsync<ImportAttendanceResponse>();
    }

    public async Task<bool> UpdateAttendanceAsync(UpdateAttendanceRequest request)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return false;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsJsonAsync("api/admin/attendance/update", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAttendanceBatchAsync(Guid batchId)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return false;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.DeleteAsync($"api/admin/attendance/batch/{batchId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<AttendanceImportBatchDto>?> GetAllAttendanceBatchesAsync()
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync("api/admin/attendance/batches");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<AttendanceImportBatchDto>>();
    }

    public async Task<AttendanceImportBatchDto?> GetAttendanceBatchByIdAsync(Guid batchId)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/attendance/batch/{batchId}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AttendanceImportBatchDto>();
    }
}