namespace OfficeCore.Client.Models.Dtos;

/// <summary>
/// DTO for task
/// </summary>
public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly? StartDate { get; set; }
    public DateOnly? DueDate { get; set; }
    
    public int Priority { get; set; }
    public string PriorityName { get; set; } = string.Empty;
    
    public int Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
    
    public int AssignmentType { get; set; }
    public string AssignmentTypeName { get; set; } = string.Empty;
    
    public Guid CreatedByUserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public Guid? ClaimedByUserId { get; set; }
    public Guid? ReviewedByUserId { get; set; }
    
    public string? CompletionNotes { get; set; }
    public string? RejectionNotes { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ClaimedAt { get; set; }
    public DateTimeOffset? StartedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? ReviewedAt { get; set; }
    
    // Created by user info
    public string CreatedByFirstName { get; set; } = string.Empty;
    public string CreatedByLastName { get; set; } = string.Empty;
    
    // Assigned to user info (if individual assignment)
    public string? AssignedToFirstName { get; set; }
    public string? AssignedToLastName { get; set; }
    public string? AssignedToPosition { get; set; }
    public string? AssignedToDepartment { get; set; }
    
    // Claimed by user info (if pool task claimed)
    public string? ClaimedByFirstName { get; set; }
    public string? ClaimedByLastName { get; set; }
    public string? ClaimedByPosition { get; set; }
    public string? ClaimedByDepartment { get; set; }
    
    // Reviewed by user info
    public string? ReviewedByFirstName { get; set; }
    public string? ReviewedByLastName { get; set; }
    
    // Computed properties
    public bool IsOverdue { get; set; }

    // Computed display properties
    public string AssignedToName => !string.IsNullOrEmpty(AssignedToFirstName) 
        ? $"{AssignedToFirstName} {AssignedToLastName}".Trim() 
        : "Unassigned";
    
    public string ClaimedByName => !string.IsNullOrEmpty(ClaimedByFirstName) 
        ? $"{ClaimedByFirstName} {ClaimedByLastName}".Trim() 
        : "Unclaimed";
    
    public string CreatedByName => $"{CreatedByFirstName} {CreatedByLastName}".Trim();
    
    public string ReviewedByName => !string.IsNullOrEmpty(ReviewedByFirstName) 
        ? $"{ReviewedByFirstName} {ReviewedByLastName}".Trim() 
        : "";
}

/// <summary>
/// Request to create a new task
/// </summary>
public class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly? StartDate { get; set; }
    public DateOnly? DueDate { get; set; }
    public int Priority { get; set; } = 3; // Normal
    public int AssignmentType { get; set; } = 1; // Individual
    public Guid? AssignedToUserId { get; set; }
}

/// <summary>
/// Request to update task status
/// </summary>
public class UpdateTaskStatusRequest
{
    public int Status { get; set; }
}

/// <summary>
/// Request to complete a task
/// </summary>
public class CompleteTaskRequest
{
    public string CompletionNotes { get; set; } = string.Empty;
}

/// <summary>
/// Request to review (reject) a task
/// </summary>
public class ReviewTaskRequest
{
    public string? RejectionNotes { get; set; }
}
