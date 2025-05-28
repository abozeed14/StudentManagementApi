using FastEndpoints;
using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Models;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Classes.Endpoints;

public class CreateClass : Endpoint<CreateClassRequest, ClassResponse>
{
    

    public override void Configure()
    {
        Post("/api/classes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateClassRequest req, CancellationToken ct)
    {
        try
        {
            IClassService? classService = TryResolve<IClassService>();

            // Create class
            var response = await classService.CreateAsync(req);

            // Map to response
          

            await SendCreatedAtAsync<GetClass>(new { Id = response.Id }, response, cancellation: ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}