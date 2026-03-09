namespace OfficeCore.Client.Models.Dtos;

public class CashAdvanceDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly RequestDate { get; set; }
    public decimal Amount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public int Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public Guid? ApprovedBy { get; set; }
    public DateTimeOffset? PaidAt { get; set; }
    public DateOnly? PayrollWeekStart { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ApprovedAt { get; set; }
    public string EmployeeFirstName { get; set; } = string.Empty;
    public string EmployeeLastName { get; set; } = string.Empty;
    public string? EmployeePosition { get; set; }
    public string? EmployeeDepartment { get; set; }
}

public class CreateCashAdvanceRequest
{
    public DateOnly RequestDate { get; set; }
    public decimal Amount { get; set; }
    public string Reason { get; set; } = string.Empty;
}
