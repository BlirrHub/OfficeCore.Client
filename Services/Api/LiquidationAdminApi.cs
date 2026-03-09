using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class LiquidationAdminApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public LiquidationAdminApi(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<List<LiquidationDto>?> GetLiquidationsByDateAsync(DateOnly date)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/liquidations?date={date:yyyy-MM-dd}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<LiquidationDto>>();
    }

    public async Task<List<LiquidationDto>?> GetPendingLiquidationsAsync()
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync("api/admin/liquidations/pending");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<LiquidationDto>>();
    }

    public async Task<LiquidationDto?> GetLiquidationAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/liquidations/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LiquidationDto>();
    }

    public async Task<LiquidationDto?> ApproveLiquidationAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsync($"api/admin/liquidations/{id}/approve", null);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LiquidationDto>();
    }

    public async Task<LiquidationDto?> RejectLiquidationAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsync($"api/admin/liquidations/{id}/reject", null);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LiquidationDto>();
    }

    public async Task<LiquidationDto?> UpdateReconciliationAsync(Guid id, UpdateLiquidationReconciliationRequest request)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsJsonAsync($"api/admin/liquidations/{id}/reconciliation", request);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LiquidationDto>();
    }
}
