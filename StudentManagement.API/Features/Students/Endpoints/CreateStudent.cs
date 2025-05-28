using FastEndpoints;
using StudentManagement.API.Features.Students.DTOs;
using StudentManagement.API.Features.Students.Validators;
using StudentManagement.API.Models;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Students.Endpoints;

public class CreateStudent : Endpoint<CreateStudentRequest, StudentResponse>
{
    public override void Configure()
    {
        Post("/api/student");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateStudentRequest req, CancellationToken ct)
    {
        try
        {
            IStudentService? studentService = TryResolve<IStudentService>();
            if (studentService == null)
            {
                await SendErrorsAsync(500, ct); // Return 500 if service resolution fails
                return;
            }

            // Create student and get response DTO directly
            var response = await studentService.CreateAsync(req);

            await SendCreatedAtAsync<GetStudent>(new { id = response.Id }, response, cancellation: ct);
        }
        catch (Exception ex)
        {
            await SendErrorsAsync(500, ct); // Return 500 with exception details
        }
    }
}