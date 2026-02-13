namespace OfficeCore.Client.Services.State;

public class AuthState
{
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
}