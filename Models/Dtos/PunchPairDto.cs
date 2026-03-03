namespace OfficeCore.Client.Models.Dtos;

/// <summary>
/// Represents a single punch pair (clock in/out session) or unpaired punch
/// </summary>
public class PunchPairDto
{
    public TimeOnly TimeIn { get; set; }
    public TimeOnly? TimeOut { get; set; }
    public TimeOnly? CalculatedTimeIn { get; set; }
    public TimeOnly? CalculatedTimeOut { get; set; }
    
    /// <summary>
    /// Duration in hours (null if TimeOut is missing)
    /// </summary>
    public decimal? Hours => TimeOut.HasValue ? (decimal)(TimeOut.Value - TimeIn).TotalHours : null;
    
    /// <summary>
    /// Duration in hours using calculated times (null if TimeOut is missing)
    /// </summary>
    public decimal? CalculatedHours => CalculatedTimeOut.HasValue && CalculatedTimeIn.HasValue 
        ? (decimal)(CalculatedTimeOut.Value - CalculatedTimeIn.Value).TotalHours 
        : null;
    
    /// <summary>
    /// True if this is an incomplete pair (missing TimeOut)
    /// </summary>
    public bool IsIncomplete => !TimeOut.HasValue;
}
