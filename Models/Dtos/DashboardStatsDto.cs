namespace OfficeCore.Client.Models.Dtos;

public class DashboardStatsDto
{
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public int InactiveEmployees { get; set; }
    
    public int PendingCashAdvances { get; set; }
    public int PendingLiquidations { get; set; }
    public int PendingTasks { get; set; }
    public int HighPriorityTasks { get; set; }
    
    public List<DepartmentStat> DepartmentStats { get; set; } = new();
    public List<RecentActivityDto> RecentActivities { get; set; } = new();
}

public class DepartmentStat
{
    public string Department { get; set; } = string.Empty;
    public int EmployeeCount { get; set; }
    public decimal Percentage { get; set; }
}

public class RecentActivityDto
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset Timestamp { get; set; }
    public string RelativeTime { get; set; } = string.Empty;
}
