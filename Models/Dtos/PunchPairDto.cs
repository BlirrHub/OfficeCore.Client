namespace OfficeCore.Client.Models.Dtos;

/// <summary>
/// Represents a single punch pair (clock in/out session) or unpaired punch
/// </summary>
public class PunchPairDto
{
    public TimeOnly TimeIn { get; set; }
    public TimeOnly? TimeOut { get; set; }
    
    /// <summary>
    /// Duration in hours (null if TimeOut is missing)
    /// </summary>
    public decimal? Hours => TimeOut.HasValue ? (decimal)(TimeOut.Value - TimeIn).TotalHours : null;
    
    /// <summary>
    /// True if this is an incomplete pair (missing TimeOut)
    /// </summary>
    public bool IsIncomplete => !TimeOut.HasValue;
}
