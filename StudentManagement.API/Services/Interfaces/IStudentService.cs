using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Features.Marks.DTOs;
using StudentManagement.API.Features.Students.DTOs;

namespace StudentManagement.API.Services.Interfaces;

public interface IStudentService
{
    Task<StudentResponse?> GetByIdAsync(int id);
    Task<List<StudentResponse>> GetAllAsync(GetAllStudentsQueryParameter queryParameter);
    Task<StudentResponse> CreateAsync(CreateStudentRequest student);
    Task<StudentResponse?> UpdateAsync(int id, UpdateStudentRequest student);
    Task<bool> DeleteAsync(int id);
    Task<StudentReportResponse> GetStudentReportAsync(int studentId);
}