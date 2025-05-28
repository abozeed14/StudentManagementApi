using FastEndpoints;
using StudentManagement.API.Features.Students.DTOs;
using StudentManagement.API.Features.Students.Validators;
using StudentManagement.API.Models;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Students.Endpoints;



public class UpdateStudent : Endpoint<UpdateStudentRequest, StudentResponse>
{
  

    public override void Configure()
    {
        Put("/api/students/{Id}");
        AllowAnonymous();
        
    }

    public override async Task HandleAsync(UpdateStudentRequest req, CancellationToken ct)
    {
        try
        {
            IStudentService? studentService = TryResolve<IStudentService>();           

            // Update student and get response DTO directly
            var response = await studentService.UpdateAsync(req.Id, req);

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