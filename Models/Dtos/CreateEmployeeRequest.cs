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
    // StdHoursPerDay removed; server applies default 8 hours

    public TimeOnly? SchedTimeIn { get; set; }
    public TimeOnly? SchedTimeOut { get; set; }
    public bool IsOTEnabled { get; set; } = false;

    // Government deduction settings (default all enabled)
    public bool HasSSSDeduction { get; set; } = true;
    public bool HasPhilHealthDeduction { get; set; } = true;
    public bool HasPagIBIGDeduction { get; set; } = true;

    public List<string> Roles { get; set; } = new();
}