namespace StudentManagement.API.Features.Classes.DTOs;

public class CreateClassRequest
{
    public string Name { get; set; } = string.Empty;
    public string Teacher { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}