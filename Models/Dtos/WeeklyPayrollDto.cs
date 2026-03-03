using OfficeCore.Client.Models.Enums;

namespace OfficeCore.Client.Models.Dtos;

/// <summary>
/// DTO representing a single employee's weekly payroll record
/// </summary>
public class WeeklyPayrollDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    // Employee info (for display)
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Position { get; set; }
    public string? Department { get; set; }
    public int? BiometricId { get; set; }
    
    // Week info
    public DateOnly WeekStart { get; set; }
    public DateOnly WeekEnd { get; set; }

    // Work calculations
    public decimal TotalHours { get; set; }
    public decimal BasicHours { get; set; }
    public decimal RegularHours { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal DailyRate { get; set; }
    public decimal BasicPay { get; set; }
    public decimal LateDeduction { get; set; }
    public decimal OvertimePay { get; set; }
    public decimal GrossPay { get; set; }

    // Supplemental adjustments (editable)
    public decimal Reimbursement { get; set; }
    public decimal HolidayPay { get; set; }
    public decimal SickLeavePay { get; set; }
    public decimal VacationLeavePay { get; set; }
    public decimal CashAdvance { get; set; }
    public decimal ELoan { get; set; }

    // Government deductions (calculated)
    public decimal SSSDeduction { get; set; }
    public decimal PhilHealthDeduction { get; set; }
    public decimal PagIBIGDeduction { get; set; }
    public bool ApplySSSDeduction { get; set; }
    public bool ApplyPhilHealthDeduction { get; set; }
    public bool ApplyPagIBIGDeduction { get; set; }
    public bool HasSSSDeduction { get; set; }
    public bool HasPhilHealthDeduction { get; set; }
    public bool HasPagIBIGDeduction { get; set; }

    // Totals
    public decimal TotalDeductions { get; set; }
    public decimal NetPay { get; set; }

    // Status & metadata
    public PayrollStatus Status { get; set; }
    public DateTimeOffset GeneratedAt { get; set; }
    public Guid? ApprovedByUserId { get; set; }
    public string? ApprovedByName { get; set; }
    public DateTimeOffset? ApprovedAt { get; set; }
    public string? Notes { get; set; }
}
