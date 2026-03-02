namespace OfficeCore.Client.Features.Employees;

public class Employee
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Department { get; set; }
    public string? Position { get; set; }

    public int? BiometricId { get; set; }
    public decimal DailyRate { get; set; }
    // StdHoursPerDay removed; client relies on server default (8h) for calculations

    public TimeOnly? SchedTimeIn { get; set; }
    public TimeOnly? SchedTimeOut { get; set; }
    public bool IsOTEnabled { get; set; }

    public bool HasSSSDeduction { get; set; } = true;
    public bool HasPhilHealthDeduction { get; set; } = true;
    public bool HasPagIBIGDeduction { get; set; } = true;

    public bool IsActive { get; set; }
    public bool MustChangePassword { get; set; }
    public List<string> Roles { get; set; } = new();
}
