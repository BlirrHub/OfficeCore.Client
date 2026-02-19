namespace OfficeCore.Client.Models.Dtos;

public class WeeklyAttendanceResponse
{
    public DateOnly WeekStart { get; set; }
    public DateOnly WeekEnd { get; set; }

    public List<EmployeeWeeklyAttendanceDto> Employees { get; set; } = new();
}
