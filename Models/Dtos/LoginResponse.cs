namespace OfficeCore.Client.Models.Dtos;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresInSeconds { get; set; }
    public string UserType { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public bool MustChangePassword { get; set; }
}