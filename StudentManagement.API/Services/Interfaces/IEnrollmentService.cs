using StudentManagement.API.Features.Enrollments.DTOs;
using StudentManagement.API.Features.Enrollments.Endpoints;
using StudentManagement.API.Models;

namespace StudentManagement.API.Services.Interfaces;

public interface IEnrollmentService
{
    Task<EnrollmentResponse?> GetEnrollmentAsync(GetEnrollmentRequest getEnrollmentModel);
    Task<EnrollmentResponse?> EnrollStudentAsync(EnrollmentRequest enrollmentModel);
    Task<bool> IsStudentEnrolledAsync(int studentId, int classId);
}