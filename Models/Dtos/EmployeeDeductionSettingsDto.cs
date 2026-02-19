namespace OfficeCore.Client.Models.Dtos;

public class EmployeeDeductionSettingsDto
{
    public int UserId { get; set; }
    public bool HasSSSDeduction { get; set; } = true;
    public bool HasPhilHealthDeduction { get; set; } = true;
    public bool HasPagIBIGDeduction { get; set; } = true;
    public DateTime EffectiveFrom { get; set; }
}
