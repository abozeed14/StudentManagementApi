using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Features.Marks.DTOs;
using StudentManagement.API.Features.Students.DTOs;
using StudentManagement.API.Infrastructure;
using StudentManagement.API.Models;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly DataStore _dataStore;


    public StudentService(DataStore dataStore)
    {
        _dataStore = dataStore;
 
    }

    public async Task<StudentResponse?> GetByIdAsync(int id)
    {
        try
        {
            if (_dataStore.Students.TryGetValue(id, out var student))
            {
                // Map domain model to DTO
                return new StudentResponse
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age
                };
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<StudentResponse>> GetAllAsync(GetAllStudentsQueryParameter queryParameter)
    {
        try
        {
            var query = _dataStore.Students.Values.AsQueryable();

            // Apply filters if provided
            if (!string.IsNullOrWhiteSpace(queryParameter.FirstName))
            {
                query = query.Where(s =>
                    s.FirstName.Contains(queryParameter.FirstName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(queryParameter.LastName))
            {
                query = query.Where(s =>
                    s.LastName.Contains(queryParameter.LastName, StringComparison.OrdinalIgnoreCase));
            }

            if (queryParameter.Age.HasValue)
            {
                query = query.Where(s => s.Age == queryParameter.Age.Value);
            }

            // Apply pagination and map to DTOs
            return query
                .Skip((queryParameter.Page - 1) * queryParameter.PageSize)
                .Take(queryParameter.PageSize)
                .Select(s => new StudentResponse
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Age = s.Age
                })
                .ToList();
        }
        catch
        {
            return new List<StudentResponse>();
        }
    }

    public async Task<StudentResponse> CreateAsync(CreateStudentRequest request)
    {
        try
        {
            // Map DTO to domain model
            var student = new Student
            {
                Id = _dataStore.GetNextStudentId(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };
            
            if (_dataStore.Students.TryAdd(student.Id, student))
            {
                // Map domain model to response DTO
                return new StudentResponse
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age
                };
            }
            
            // Return empty response with default values if failed
            return new StudentResponse();
        }
        catch
        {
            // Return empty response with default values if exception occurs
            return new StudentResponse();
        }
    }

    public async Task<StudentResponse?> UpdateAsync(int id, UpdateStudentRequest request)
    {
        try
        {
            if (!_dataStore.Students.ContainsKey(id))
            {
                return null;
            }

            // Map DTO to domain model
            var student = new Student
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };

            if (_dataStore.Students.TryUpdate(id, student, _dataStore.Students[id]))
            {
                // Map domain model to response DTO
                return new StudentResponse
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age
                };
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            return _dataStore.Students.TryRemove(id, out _);
        }
        catch
        {
            return false;
        }
    }

    public async Task<StudentReportResponse> GetStudentReportAsync(int studentId)
    {
        try
        {
            // Check if student exists
            if (!_dataStore.Students.TryGetValue(studentId, out var student))
            {
                return new StudentReportResponse();
            }

            // Get all enrollments for the student
            var enrollments = _dataStore.Enrollments.Values
                .Where(e => e.StudentId == studentId)
                .ToList();

            // Get classes for these enrollments
            var classes = new List<ClassResponse>();
            foreach (var enrollment in enrollments)
            {
                if (_dataStore.Classes.TryGetValue(enrollment.ClassId, out var classObj))
                {
                    classes.Add(new ClassResponse
                    {
                        Id = classObj.Id,
                        Name = classObj.Name,
                        Teacher = classObj.Teacher,
                        Description = classObj.Description
                    });
                }
            }

            // Get marks for the student
            var marks = _dataStore.Marks.Values
                .Where(m => m.StudentId == studentId)
                .Select(m => new MarkResponse
                {
                    StudentId = m.StudentId,
                    ClassId = m.ClassId,
                    ExamMark = m.ExamMark,
                    AssignmentMark = m.AssignmentMark,
                    TotalMark = m.TotalMark
                })
                .ToList();

            // Calculate average mark across all classes
            double? averageMark = null;
            if (marks.Any())
            {
                averageMark = marks.Average(m => m.TotalMark);
            }

            // Create and return the response DTO
            return new StudentReportResponse
            {
                Student = new StudentResponse
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age
                },
                EnrolledClasses = classes,
                Marks = marks,
                AverageMark = averageMark
            };
        }
        catch
        {
            return new StudentReportResponse();
        }
    }
}