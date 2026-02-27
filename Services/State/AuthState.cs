using System.Text.Json;
using Microsoft.JSInterop;

namespace OfficeCore.Client.Services.State;

public class AuthState
{
    private const string TokenKey = "auth.token";
    private const string UserTypeKey = "auth.userType";
    private const string RolesKey = "auth.roles";

    public string? AccessToken { get; private set; }
    public string? UserType { get; private set; }
    public IReadOnlyList<string> Roles { get; private set; } = new List<string>();

    public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);

    public void Set(string token, string userType, IReadOnlyList<string> roles)
    {
        AccessToken = token;
        UserType = userType;
        Roles = roles;
    }

    public void Clear()
    {
        AccessToken = null;
        UserType = null;
        Roles = new List<string>();
    }

    public async Task SaveAsync(IJSRuntime js)
    {
        if (js is null) return;

        if (!string.IsNullOrEmpty(AccessToken))
        {
            await js.InvokeVoidAsync("localStorage.setItem", TokenKey, AccessToken);
            await js.InvokeVoidAsync("localStorage.setItem", UserTypeKey, UserType ?? string.Empty);
            var rolesJson = JsonSerializer.Serialize(Roles);
            await js.InvokeVoidAsync("localStorage.setItem", RolesKey, rolesJson);
        }
        else
        {
            await js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            await js.InvokeVoidAsync("localStorage.removeItem", UserTypeKey);
            await js.InvokeVoidAsync("localStorage.removeItem", RolesKey);
        }
    }

    public async Task LoadAsync(IJSRuntime js)
    {
        if (js is null) return;

        var token = await js.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        if (string.IsNullOrEmpty(token))
        {
            Clear();
            return;
        }

        var userType = await js.InvokeAsync<string?>("localStorage.getItem", UserTypeKey);
        var rolesJson = await js.InvokeAsync<string?>("localStorage.getItem", RolesKey);
        IReadOnlyList<string> roles = new List<string>();
        if (!string.IsNullOrEmpty(rolesJson))
        {
            try
            {
                roles = JsonSerializer.Deserialize<List<string>>(rolesJson) ?? new List<string>();
            }
            catch
            {
                roles = new List<string>();
            }
        }

        Set(token!, userType ?? string.Empty, roles);
    }
}