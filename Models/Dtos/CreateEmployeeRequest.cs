namespace OfficeCore.Client.Models.Dtos;

public class CreateEmployeeRequest
{
    public string Username { get; set; } = "";
    public string TempPassword { get; set; } = "";

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public string? Department { get; set; }
    public string? Position { get; set; }

    public List<string> Roles { get; set; } = new();
}