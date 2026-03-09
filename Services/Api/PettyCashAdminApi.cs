using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class PettyCashAdminApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public PettyCashAdminApi(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<PettyCashFundDto?> GetFundAsync(DateOnly date)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/petty-cash/{date:yyyy-MM-dd}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PettyCashFundDto>();
    }

    public async Task<PettyCashFundDto?> GetCurrentBalanceAsync()
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync("api/admin/petty-cash/current");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PettyCashFundDto>();
    }

    public async Task<List<PettyCashFundDto>?> GetFundHistoryAsync(DateOnly startDate, DateOnly endDate)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/petty-cash/history?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<PettyCashFundDto>>();
    }

    public async Task<PettyCashFundDto?> UpdateFundAsync(DateOnly date, UpdatePettyCashFundRequest request)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsJsonAsync($"api/admin/petty-cash/{date:yyyy-MM-dd}", request);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PettyCashFundDto>();
    }

    public async Task<PettyCashFundDto?> RecalculateBalanceAsync(DateOnly date)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PostAsync($"api/admin/petty-cash/{date:yyyy-MM-dd}/recalculate", null);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PettyCashFundDto>();
    }
}
