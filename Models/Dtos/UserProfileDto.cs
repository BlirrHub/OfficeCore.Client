namespace OfficeCore.Client.Models.Dtos;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<string> Roles { get; set; } = new();

    // Employee profile fields (null for admin-only users)
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public int? BiometricId { get; set; }
    public decimal? DailyRate { get; set; }
    public TimeOnly? SchedTimeIn { get; set; }
    public TimeOnly? SchedTimeOut { get; set; }
    public bool IsOTEnabled { get; set; }
}
