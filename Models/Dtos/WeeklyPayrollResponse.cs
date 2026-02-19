namespace OfficeCore.Client.Models.Dtos;

public class WeeklyPayrollResponse
{
    public DateTime WeekStart { get; set; }
    public DateTime WeekEnd { get; set; }
    public List<WeeklyPayrollDto> Employees { get; set; } = new();

    // Aggregate totals
    public decimal TotalBasicPay { get; set; }
    public decimal TotalOvertimePay { get; set; }
    public decimal TotalGrossPay { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal TotalNetPay { get; set; }

    // Status counts
    public int DraftCount { get; set; }
    public int ApprovedCount { get; set; }
    public int PaidCount { get; set; }
}
