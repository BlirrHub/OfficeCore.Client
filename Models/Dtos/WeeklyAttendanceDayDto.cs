namespace OfficeCore.Client.Models.Dtos;

public class WeeklyAttendanceDayDto
{
    public DateOnly Date { get; set; }
    public TimeOnly? TimeIn { get; set; }
    public TimeOnly? TimeOut { get; set; }
    public int PunchCount { get; set; }
    public bool IsIncomplete { get; set; }
}
