using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class CashAdvanceAdminApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public CashAdvanceAdminApi(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<List<CashAdvanceDto>?> GetPendingRequestsAsync()
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync("api/admin/cash-advances/pending");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<CashAdvanceDto>>();
    }

    public async Task<List<CashAdvanceDto>?> GetRequestsAsync(DateOnly? startDate = null, DateOnly? endDate = null, int? status = null)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var url = "api/admin/cash-advances";
        var queryParams = new List<string>();
        
        if (startDate.HasValue)
            queryParams.Add($"startDate={startDate.Value:yyyy-MM-dd}");
        if (endDate.HasValue)
            queryParams.Add($"endDate={endDate.Value:yyyy-MM-dd}");
        if (status.HasValue)
            queryParams.Add($"status={status.Value}");

        if (queryParams.Any())
            url += "?" + string.Join("&", queryParams);

        var response = await _http.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<CashAdvanceDto>>();
    }

    public async Task<CashAdvanceDto?> GetRequestAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/cash-advances/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CashAdvanceDto>();
    }

    public async Task<CashAdvanceDto?> ApproveRequestAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsync($"api/admin/cash-advances/{id}/approve", null);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CashAdvanceDto>();
    }

    public async Task<CashAdvanceDto?> RejectRequestAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsync($"api/admin/cash-advances/{id}/reject", null);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CashAdvanceDto>();
    }

    public async Task<CashAdvanceDto?> MarkAsPaidAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsync($"api/admin/cash-advances/{id}/mark-paid", null);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CashAdvanceDto>();
    }
}
