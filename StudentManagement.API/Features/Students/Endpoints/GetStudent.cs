using FastEndpoints;
using StudentManagement.API.Features.Students.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Students.Endpoints;


public class GetStudent : Endpoint<GetStudentRequest, StudentResponse>
{
    private readonly IStudentService _studentService;

    public GetStudent(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public override void Configure()
    {
        Get("/api/student/{Id}");
        AllowAnonymous();
      
    }

    public override async Task HandleAsync(GetStudentRequest req, CancellationToken ct)
    {
        try
        {
            var response = await _studentService.GetByIdAsync(req.Id);

            if (response == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendOkAsync(response, ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}