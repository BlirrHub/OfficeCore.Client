namespace OfficeCore.Client.Models.Dtos;

public class UpdateDeductionRequest
{
    public decimal Reimbursement { get; set; }
    public decimal HolidayPay { get; set; }
    public decimal SickLeavePay { get; set; }
    public decimal VacationLeavePay { get; set; }
    public decimal CashAdvance { get; set; }
    public decimal ELoan { get; set; }
    public string? Notes { get; set; }
}
