namespace StudentManagement.API.Models;

public class Enrollment
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
}