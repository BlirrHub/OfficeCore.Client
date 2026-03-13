using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using OfficeCore.Client.Models.Dtos;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Api;

public class LiquidationAdminApi
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public string? LastError { get; private set; }

    public LiquidationAdminApi(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    public async Task<List<LiquidationDto>?> GetLiquidationsByDateAsync(DateOnly date)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        LastError = null;

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

        LastError = null;

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

        LastError = null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.GetAsync($"api/admin/liquidations/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LiquidationDto>();
    }

    public async Task<LiquidationDto?> CreateLiquidationAsync(CreateLiquidationRequest request, IReadOnlyList<IBrowserFile?>? receipts = null)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        LastError = null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        using var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(request.UserId?.ToString() ?? string.Empty), "UserId");
        formData.Add(new StringContent(request.Date.ToString("yyyy-MM-dd")), "Date");
        formData.Add(new StringContent(request.PettyCashReceived.ToString(System.Globalization.CultureInfo.InvariantCulture)), "PettyCashReceived");

        if (request.IssuedBy.HasValue)
        {
            formData.Add(new StringContent(request.IssuedBy.Value.ToString()), "IssuedBy");
        }

        if (!string.IsNullOrWhiteSpace(request.IssuedByName))
        {
            formData.Add(new StringContent(request.IssuedByName), "IssuedByName");
        }

        for (int index = 0; index < request.Entries.Count; index++)
        {
            var entry = request.Entries[index];
            formData.Add(new StringContent(entry.Particulars), $"Entries[{index}].Particulars");
            formData.Add(new StringContent(entry.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture)), $"Entries[{index}].Amount");

            if (receipts != null && index < receipts.Count && receipts[index] != null)
            {
                var file = receipts[index]!;
                var streamContent = new StreamContent(file.OpenReadStream(10_000_000));
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                formData.Add(streamContent, "receipts", file.Name);
            }
        }

        var response = await _http.PostAsync("api/admin/liquidations", formData);
        if (!response.IsSuccessStatusCode)
        {
            LastError = await TryReadErrorMessageAsync(response);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LiquidationDto>();
    }

    public async Task<LiquidationDto?> ApproveLiquidationAsync(Guid id)
    {
        if (string.IsNullOrWhiteSpace(_auth.AccessToken))
            return null;

        LastError = null;

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

        LastError = null;

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

        LastError = null;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.AccessToken);

        var response = await _http.PutAsJsonAsync($"api/admin/liquidations/{id}/reconciliation", request);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LiquidationDto>();
    }

    private static async Task<string?> TryReadErrorMessageAsync(HttpResponseMessage response)
    {
        try
        {
            var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            if (payload != null)
            {
                if (payload.TryGetValue("Message", out var message) || payload.TryGetValue("message", out message))
                {
                    return message;
                }
            }
        }
        catch
        {
        }

        return $"Request failed with status {(int)response.StatusCode}.";
    }
}
