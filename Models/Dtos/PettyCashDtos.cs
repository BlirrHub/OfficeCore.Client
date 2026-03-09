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
