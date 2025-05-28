using StudentManagement.API.Features.Marks.DTOs;
using StudentManagement.API.Infrastructure;
using StudentManagement.API.Models;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Services.Implementations;

public class MarkService : IMarkService
{
    private readonly DataStore _dataStore;
    private readonly IEnrollmentService _enrollmentService;

    public MarkService(DataStore dataStore, IEnrollmentService enrollmentService)
    {
        _dataStore = dataStore;
        _enrollmentService = enrollmentService;
    }

    public async Task<MarkResponse?> GetMarkAsync(GetMarkRequest markRequest)
    {
        try
        {
            var key = DataStore.GenerateMarkKey(markRequest.StudentId, markRequest.ClassId);
            if (_dataStore.Marks.TryGetValue(key, out var mark))
            {
                return new MarkResponse
                {
                    StudentId = mark.StudentId,
                    ClassId = mark.ClassId,
                    ExamMark = mark.ExamMark,
                    AssignmentMark = mark.AssignmentMark,
                    TotalMark = mark.TotalMark,
                };
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<MarkResponse?> RecordMarkAsync(MarkRequest markRequest)
    {
        try
        {
            // Check if student is enrolled in the class
            var isEnrolled = await _enrollmentService.IsStudentEnrolledAsync(markRequest.StudentId, markRequest.ClassId);
            if (!isEnrolled)
            {
                return null;
            }

            var key = DataStore.GenerateMarkKey(markRequest.StudentId, markRequest.ClassId);
            var mark = new Mark
            {
                StudentId = markRequest.StudentId,
                ClassId = markRequest.ClassId,
                ExamMark = markRequest.ExamMark,
                AssignmentMark = markRequest.AssignmentMark
            };

            // Update if exists, add if not
            _dataStore.Marks[key] = mark;
            return new MarkResponse
            {
                StudentId = markRequest.StudentId, 
                ClassId = markRequest.ClassId,
                ExamMark = markRequest.ExamMark,
                AssignmentMark = markRequest.AssignmentMark
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while recording the mark: {ex.Message}");
        }
    }
}