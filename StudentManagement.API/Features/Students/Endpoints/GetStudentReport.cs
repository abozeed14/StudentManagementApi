using FastEndpoints;
using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Features.Marks.DTOs;
using StudentManagement.API.Features.Students.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Students.Endpoints;



public class GetStudentReport : Endpoint<GetStudentReportRequest, StudentReportResponse>
{
    

    public override void Configure()
    {
        Get("/api/student/{StudentId}/report");
        AllowAnonymous();
       
    }

    public override async Task HandleAsync(GetStudentReportRequest req, CancellationToken ct)
    {
        try
        {
            IStudentService? studentService = TryResolve<IStudentService>();
            // Get student
            var student = await studentService.GetByIdAsync(req.StudentId);
            if (student == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            // Get report data directly as DTO
            var response = await studentService.GetStudentReportAsync(req.StudentId);

            await SendOkAsync(response, ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}