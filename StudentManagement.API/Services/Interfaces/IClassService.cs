using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Models;

namespace StudentManagement.API.Services.Interfaces;

public interface IClassService
{
    Task<ClassResponse?> GetByIdAsync(int id);
    Task<List<ClassResponse>> GetAllAsync(GetAllClassesQueryParameter queryParameter);
    Task<ClassResponse> CreateAsync(CreateClassRequest classobj);
    Task<bool> DeleteAsync(int id);
    Task<ClassAverageResponse?> GetAverageMarksAsync(int classId , ClassResponse classModel);
}