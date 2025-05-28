using FastEndpoints;
using StudentManagement.API.Features.Students.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Students.Endpoints;



public class GetAllStudents : Endpoint<GetAllStudentsQueryParameter, GetAllStudentsResponse>
{
   
    public override void Configure()
    {
        Get("/api/students");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllStudentsQueryParameter req, CancellationToken ct)
    {
        try
        {
            IStudentService? studentService = TryResolve<IStudentService>();
            // Ensure valid pagination parameters
            req.Page = req.Page <= 0 ? 1 : req.Page;
            req.PageSize = req.PageSize <= 0 ? 10 : (req.PageSize > 100 ? 100 : req.PageSize);

            // Get students with filtering and pagination
            var students = await studentService.GetAllAsync(req);

            // Create response
            var response = new GetAllStudentsResponse
            {
                Students = students,
                Page = req.Page,
                PageSize = req.PageSize
            };

            await SendOkAsync(response, ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}