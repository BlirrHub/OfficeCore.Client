namespace OfficeCore.Client.Models.Dtos;

public class UpdateAttendanceRequest
{
    public Guid UserId { get; set; }
    public DateOnly WorkDate { get; set; }
    
    /// <summary>
    /// List of all punch times for the day (will be paired automatically)
    /// </summary>
    public List<TimeOnly> Punches { get; set; } = new();
    
    // Legacy fields for backward compatibility
    public TimeOnly? TimeIn { get; set; }
    public TimeOnly? TimeOut { get; set; }
}
