using System.Collections.Concurrent;
using StudentManagement.API.Models;

namespace StudentManagement.API.Infrastructure;

public class DataStore
{
    public ConcurrentDictionary<int, Student> Students { get; } = new();
    public ConcurrentDictionary<int, Class> Classes { get; } = new();
    public ConcurrentDictionary<string, Enrollment> Enrollments { get; } = new();
    public ConcurrentDictionary<string, Mark> Marks { get; } = new();

    // Counters for generating IDs
    private int _studentIdCounter = 1;
    private int _classIdCounter = 1;

    public int GetNextStudentId() => Interlocked.Increment(ref _studentIdCounter);
    public int GetNextClassId() => Interlocked.Increment(ref _classIdCounter);

    // Helper method to generate a unique key for enrollments
    public static string GenerateEnrollmentKey(int studentId, int classId) => $"{studentId}_{classId}";

    // Helper method to generate a unique key for marks
    public static string GenerateMarkKey(int studentId, int classId) => $"{studentId}_{classId}";
}