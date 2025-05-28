using System.Text.Json.Serialization;

namespace StudentManagement.API.Features.Enrollments.DTOs;

public class EnrollmentResponse
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    
    [JsonPropertyName("enrolledAt")]
    public DateTime EnrolledAt { get; set; }
}