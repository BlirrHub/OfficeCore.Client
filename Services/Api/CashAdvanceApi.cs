using System.Net.Http.Headers;
using System.Net.Http.Json;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class CashAdvanceApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public CashAdvanceApi(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<CashAdvanceDto?> RequestCashAdvanceAsync(CreateCashAdvanceRequest request)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PostAsJsonAsync("api/cash-advance/request", request);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CashAdvanceDto>();
    }

    public async Task<List<CashAdvanceDto>?> GetMyRequestsAsync(DateOnly? startDate = null, DateOnly? endDate = null)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var url = "api/cash-advance/my-requests";
        var queryParams = new List<string>();
        
        if (startDate.HasValue)
            queryParams.Add($"startDate={startDate.Value:yyyy-MM-dd}");
        if (endDate.HasValue)
            queryParams.Add($"endDate={endDate.Value:yyyy-MM-dd}");

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

        var response = await _http.GetAsync($"api/cash-advance/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CashAdvanceDto>();
    }
}
