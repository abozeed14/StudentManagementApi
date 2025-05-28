namespace StudentManagement.API.Features.Marks.DTOs;

public class MarkRequest
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public double ExamMark { get; set; }
    public double AssignmentMark { get; set; }
}