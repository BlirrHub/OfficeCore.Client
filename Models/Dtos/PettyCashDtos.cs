namespace OfficeCore.Client.Models.Dtos;

public class PettyCashFundDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal ClosingBalance { get; set; }
    public decimal Deposits { get; set; }
    public decimal Withdrawals { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class UpdatePettyCashFundRequest
{
    public decimal Deposits { get; set; }
    public decimal Withdrawals { get; set; }
    public string? Notes { get; set; }
}

public class PettyCashTransactionDetailsDto
{
    public DateOnly Date { get; set; }
    public List<CashAdvanceTransactionDto> CashAdvancesPaid { get; set; } = new();
    public List<LiquidationTransactionDto> LiquidationsIssued { get; set; } = new();
    public List<ManualTransactionDto> ManualTransactions { get; set; } = new();
}

public class CashAdvanceTransactionDto
{
    public Guid Id { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTimeOffset PaidAt { get; set; }
    public string? Reason { get; set; }
}

public class LiquidationTransactionDto
{
    public Guid Id { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public decimal PettyCashReceived { get; set; }
    public decimal AmountReturned { get; set; }
    public DateOnly Date { get; set; }
}

public class ManualTransactionDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
