namespace OfficeCore.Client.Models.Dtos;

public class AttendanceImportBatchDto
{
    public Guid Id { get; set; }
    public DateTimeOffset ImportedAtUtc { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public DateOnly WeekStart { get; set; }
    public DateOnly WeekEnd { get; set; }
    public int InsertedCount { get; set; }
    public int SkippedDuplicateCount { get; set; }
    public int UnmatchedEmployeeCount { get; set; }
    public int ErrorCount { get; set; }
}
