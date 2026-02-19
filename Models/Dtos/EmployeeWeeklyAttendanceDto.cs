namespace OfficeCore.Client.Models.Dtos;

public class EmployeeWeeklyAttendanceDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = "";

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public string? Department { get; set; }
    public int? BiometricId { get; set; }
    public TimeOnly? SchedTimeIn { get; set; }
    public TimeOnly? SchedTimeOut { get; set; }
    public bool IsOTEnabled { get; set; }
    public List<WeeklyAttendanceDayDto> Days { get; set; } = new();
}
