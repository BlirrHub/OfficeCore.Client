namespace OfficeCore.Client.Models.Dtos;

public class PositionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreatePositionRequest
{
    public string Name { get; set; } = string.Empty;
}

public class UpdatePositionRequest
{
    public string Name { get; set; } = string.Empty;
}
