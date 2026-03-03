namespace OfficeCore.Client.Models.Dtos;

public class WeeklyAttendanceDayDto
{
    public DateOnly Date { get; set; }
    public TimeOnly? TimeIn { get; set; }
    public TimeOnly? TimeOut { get; set; }
    public TimeOnly? CalculatedTimeIn { get; set; }
    public TimeOnly? CalculatedTimeOut { get; set; }
    public int PunchCount { get; set; }
    public bool IsIncomplete { get; set; }
    
    /// <summary>
    /// List of paired punch in/out sessions for the day
    /// </summary>
    public List<PunchPairDto> PunchPairs { get; set; } = new();
}
