using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Infrastructure;
using StudentManagement.API.Models;
using StudentManagement.API.Services.Interfaces;
using System.Linq;

namespace StudentManagement.API.Services.Implementations;

public class ClassService : IClassService
{
    private readonly DataStore _dataStore;

    public ClassService(DataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public async Task<ClassResponse?> GetByIdAsync(int id)
    {
        try
        {
            if (_dataStore.Classes.TryGetValue(id, out var classObj))
            {
                return new ClassResponse
                {
                    Id = id,
                    Name = classObj.Name,
                    Teacher = classObj.Teacher,
                    Description = classObj.Description,
                };
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<ClassResponse>> GetAllAsync(GetAllClassesQueryParameter queryParameter)
    {
        try
        {
            var query = _dataStore.Classes.Values.AsQueryable();

            // Apply filters if provided
            if (!string.IsNullOrWhiteSpace(queryParameter.Name))
            {
                query = query.Where(c => c.Name.Contains(queryParameter.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(queryParameter.Teacher))
            {
                query = query.Where(c => c.Teacher.Contains(queryParameter.Teacher, StringComparison.OrdinalIgnoreCase));
            }

            // Apply pagination
            return query
                .Skip((queryParameter.Page - 1) * queryParameter.PageSize)
                .Take(queryParameter.PageSize)
                .Select(c => new ClassResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Teacher = c.Teacher,
                    Description = c.Description
                })
                .ToList();
        }
        catch
        {
            return new List<ClassResponse>();
        }
    }

    public async Task<ClassResponse> CreateAsync(CreateClassRequest classObj)
    {
        try
        { 
            Class addedClass = new Class()
            {
                Id = _dataStore.GetNextClassId(),
                Name = classObj.Name,
                Teacher = classObj.Teacher,
                Description = classObj.Description,
            };
            if (_dataStore.Classes.TryAdd(addedClass.Id, addedClass))
            {
                return new ClassResponse
                {
                    Id = addedClass.Id,
                    Name = addedClass.Name,
                    Teacher = addedClass.Teacher,
                    Description = addedClass.Description,
                };
            }
            throw new Exception("Failed to add class");
        }
        catch
        {
            throw new Exception("An error occurred while creating the class");
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            return _dataStore.Classes.TryRemove(id, out _);
        }
        catch
        {
            return false;
        }
    }

    public async Task<ClassAverageResponse?> GetAverageMarksAsync(int classId, ClassResponse classModel)
    {
        try
        {
            // Check if class exists
            if (!_dataStore.Classes.ContainsKey(classId))
            {
                return null;
            }

            // Get all marks for this class
            var marks = _dataStore.Marks.Values
                .Where(m => m.ClassId == classId)
                .ToList();

            if (!marks.Any())
            {
                return null; // No marks recorded for this class
            }
            var averageMark = marks.Average(m => m.TotalMark);

            return new ClassAverageResponse
            {
                ClassId = classId,
                AverageMark = averageMark,
                ClassName = classModel.Name,
            };
        }
        catch
        {
            return null;
        }
    }
}