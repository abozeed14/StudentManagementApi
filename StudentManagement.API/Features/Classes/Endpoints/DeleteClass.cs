using FastEndpoints;
using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Classes.Endpoints;



public class DeleteClass : Endpoint<DeleteClassRequest>
{
    
    public override void Configure()
    {
        Delete("/api/classes/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteClassRequest req, CancellationToken ct)
    {
        try
        {
            IClassService? classService = TryResolve<IClassService>();
            var result = await classService.DeleteAsync(req.Id);

            if (!result)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendNoContentAsync(ct);
        }
        catch (Exception )
        {
            await SendErrorsAsync(400, ct);
        }
    }
}