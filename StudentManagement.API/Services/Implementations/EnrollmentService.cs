using StudentManagement.API.Features.Enrollments.DTOs;
using StudentManagement.API.Features.Enrollments.Endpoints;
using StudentManagement.API.Infrastructure;
using StudentManagement.API.Models;
using StudentManagement.API.Services.Interfaces;
using System.Collections.Generic;

namespace StudentManagement.API.Services.Implementations;

public class EnrollmentService : IEnrollmentService
{
    private readonly DataStore _dataStore;
    private readonly IStudentService _studentService;
    private readonly IClassService _classService;

    public EnrollmentService(DataStore dataStore, IStudentService studentService, IClassService classService)
    {
        _dataStore = dataStore;
        _studentService = studentService;
        _classService = classService;
    }

    public async Task<EnrollmentResponse?> GetEnrollmentAsync(GetEnrollmentRequest getEnrollmentModel)
    {
        try
        {
            var key = DataStore.GenerateEnrollmentKey(getEnrollmentModel.StudentId, getEnrollmentModel.ClassId);
            if ( _dataStore.Enrollments.TryGetValue(key, out var enrollment))
            {
                return new EnrollmentResponse
                {
                    ClassId = enrollment.ClassId,
                    StudentId = enrollment.StudentId,
                    EnrolledAt = enrollment.EnrolledAt,
                };
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<EnrollmentResponse?> EnrollStudentAsync(EnrollmentRequest enrollmentModel)
    {
        try
        {
            // Check if student exists
            var student = await _studentService.GetByIdAsync(enrollmentModel.StudentId);
            if (student == null)
            {
                return null;
            }
            
            // Check if class exists
            var classObj = await _classService.GetByIdAsync(enrollmentModel.ClassId);
            if (classObj == null)
            {
                return null;
            }

            // Check if already enrolled
            var key = DataStore.GenerateEnrollmentKey(enrollmentModel.StudentId, enrollmentModel.ClassId);
            if (_dataStore.Enrollments.TryGetValue(key, out _))
            {
                return null;
            }

            // Create enrollment
            var enrollment = new Enrollment
            {
                StudentId = enrollmentModel.StudentId,
                ClassId = enrollmentModel.ClassId,
                EnrolledAt = DateTime.UtcNow
            };

            if (_dataStore.Enrollments.TryAdd(key, enrollment))
            {
                return new EnrollmentResponse
                {
                    StudentId = student.Id,
                    ClassId = classObj.Id,
                    EnrolledAt = enrollment.EnrolledAt,
                };
            }

            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> IsStudentEnrolledAsync(int studentId, int classId)
    {
        try
        {
            var key = DataStore.GenerateEnrollmentKey(studentId, classId);
            return _dataStore.Enrollments.ContainsKey(key);
        }
        catch
        {
            return false;
        }
    }
}