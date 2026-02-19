namespace OfficeCore.Client.Models.Dtos;

public class UpdateAttendanceRequest
{
    public Guid UserId { get; set; }
    public DateOnly WorkDate { get; set; }
    public TimeOnly? TimeIn { get; set; }
    public TimeOnly? TimeOut { get; set; }
}
