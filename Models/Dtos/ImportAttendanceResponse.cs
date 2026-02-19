namespace OfficeCore.Client.Models.Dtos;

public class ImportAttendanceResponse
{
    public Guid BatchId { get; set; }
    public DateOnly WeekStart { get; set; }
    public DateOnly WeekEnd { get; set; }

    public int InsertedCount { get; set; }
    public int SkippedDuplicateCount { get; set; }
    public int UnmatchedEmployeeCount { get; set; }
    public int ErrorCount { get; set; }

    public List<AttendanceImportErrorDto> SampleErrors { get; set; } = new();
}
