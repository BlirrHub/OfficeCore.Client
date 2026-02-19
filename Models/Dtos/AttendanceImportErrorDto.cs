namespace OfficeCore.Client.Models.Dtos;

public class AttendanceImportErrorDto
{
    public int? BiometricId { get; set; }
    public DateOnly? WorkDate { get; set; }
    public string Message { get; set; } = "";
    public string? Reference { get; set; }
}
