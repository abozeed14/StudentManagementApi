namespace StudentManagement.API.Features.Classes.DTOs;

public class ClassAverageResponse
{
    public int ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public double AverageMark { get; set; }
}