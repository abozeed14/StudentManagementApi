namespace StudentManagement.API.Models;

public class Mark
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public double ExamMark { get; set; }
    public double AssignmentMark { get; set; }

    public double TotalMark => ExamMark + AssignmentMark;
}