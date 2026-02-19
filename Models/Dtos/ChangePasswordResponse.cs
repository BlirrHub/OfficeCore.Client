namespace OfficeCore.Client.Models.Dtos;

public class ChangePasswordResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresInSecond { get; set; }
}