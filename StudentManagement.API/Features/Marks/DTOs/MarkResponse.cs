namespace StudentManagement.API.Features.Marks.DTOs;

public class MarkResponse
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public double ExamMark { get; set; }
    public double AssignmentMark { get; set; }
    public double TotalMark { get; set; }
}