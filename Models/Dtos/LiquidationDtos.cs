namespace OfficeCore.Client.Models.Dtos;

public class LiquidationEntryDto
{
    public Guid Id { get; set; }
    public string Particulars { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int SequenceOrder { get; set; }
    public string? ReceiptImagePath { get; set; }
    public string? ReceiptImageUrl { get; set; }
}

public class LiquidationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public decimal PettyCashReceived { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal AmountReturned { get; set; }
    public decimal ReimbursementDue { get; set; }
    public int Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public Guid? IssuedBy { get; set; }
    public string? IssuedByName { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ApprovedAt { get; set; }
    public string EmployeeFirstName { get; set; } = string.Empty;
    public string EmployeeLastName { get; set; } = string.Empty;
    public string? EmployeePosition { get; set; }
    public string? EmployeeDepartment { get; set; }
    public List<LiquidationEntryDto> Entries { get; set; } = new();
}

public class CreateLiquidationRequest
{
    public DateOnly Date { get; set; }
    public decimal PettyCashReceived { get; set; }
    public Guid? IssuedBy { get; set; }
    public string? IssuedByName { get; set; }
    public List<CreateLiquidationEntryDto> Entries { get; set; } = new();
}

public class CreateLiquidationEntryDto
{
    public string Particulars { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? ReceiptImagePath { get; set; }
}

public class UpdateLiquidationReconciliationRequest
{
    public decimal AmountReturned { get; set; }
    public decimal ReimbursementDue { get; set; }
}
