namespace OfficeCore.Client.Models.Dtos;

public class CreateEmployeeRequest
{
    public string Username { get; set; } = "";
    public string TempPassword { get; set; } = "";

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public string? Department { get; set; }
    public string? Position { get; set; }

    public int? BiometricId { get; set; }
    public decimal DailyRate { get; set; } = 0m;
    public decimal StdHoursPerDay { get; set; } = 8m;

    public TimeOnly? SchedTimeIn { get; set; }
    public TimeOnly? SchedTimeOut { get; set; }
    public bool IsOTEnabled { get; set; } = false;

    public List<string> Roles { get; set; } = new();
}