using FastEndpoints;
using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Classes.Endpoints;



public class GetClass : Endpoint<GetClassRequest, ClassResponse>
{
    public override void Configure()
    {
        Get("/api/classes/{Id}");
        AllowAnonymous();
       
    }

    public override async Task HandleAsync(GetClassRequest req, CancellationToken ct)
    {
        try
        {
            IClassService classService = TryResolve<IClassService>();
            var classModel = await classService.GetByIdAsync(req.Id);

            if (classModel == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var response = new ClassResponse
            {
                Id = classModel.Id,
                Name = classModel.Name,
                Teacher = classModel.Teacher,
                Description = classModel.Description
            };

            await SendOkAsync(response, ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}