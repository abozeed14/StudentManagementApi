using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Features.Marks.DTOs;

namespace StudentManagement.API.Features.Students.DTOs;

public class StudentReportResponse
{
    public StudentResponse Student { get; set; } = new();
    public List<ClassResponse> EnrolledClasses { get; set; } = new();
    public List<MarkResponse> Marks { get; set; } = new();
    public double? AverageMark { get; set; }
}