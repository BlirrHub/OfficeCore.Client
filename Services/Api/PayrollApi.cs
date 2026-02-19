using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class PayrollApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public PayrollApi(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<WeeklyPayrollResponse?> GenerateWeeklyPayrollAsync(DateOnly weekStart)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PostAsync($"api/admin/payroll/weekly/generate?weekStart={weekStart:yyyy-MM-dd}", null);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<WeeklyPayrollResponse>();
    }

    public async Task<WeeklyPayrollResponse?> GetWeeklyPayrollAsync(DateOnly weekStart)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/payroll/weekly?weekStart={weekStart:yyyy-MM-dd}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<WeeklyPayrollResponse>();
    }

    public async Task<WeeklyPayrollDto?> UpdateDeductionsAsync(Guid userId, DateOnly weekStart, UpdateDeductionRequest request)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsJsonAsync(
            $"api/admin/payroll/weekly/{userId}/deductions?weekStart={weekStart:yyyy-MM-dd}", 
            request);
        
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<WeeklyPayrollDto>();
    }

    public async Task<WeeklyPayrollDto?> ApprovePayrollAsync(Guid userId, DateOnly weekStart)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsync(
            $"api/admin/payroll/weekly/{userId}/approve?weekStart={weekStart:yyyy-MM-dd}", 
            null);
        
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<WeeklyPayrollDto>();
    }
}
