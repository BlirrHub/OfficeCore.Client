namespace OfficeCore.Client.Models.Dtos;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateRoleRequest
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateRoleRequest
{
    public string Name { get; set; } = string.Empty;
}
