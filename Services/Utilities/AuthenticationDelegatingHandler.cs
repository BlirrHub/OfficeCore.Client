using Microsoft.AspNetCore.Components;
using OfficeCore.Client.Services.State;

namespace OfficeCore.Client.Services.Utilities;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly AuthState _authState;
    private readonly NavigationManager _navigationManager;

    public AuthenticationDelegatingHandler(AuthState authState, NavigationManager navigationManager)
    {
        _authState = authState;
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        // If we get a 401 Unauthorized, clear auth state and redirect to login
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            _authState.Clear();
            _navigationManager.NavigateTo("/auth/login", forceLoad: true);
        }

        return response;
    }
}
