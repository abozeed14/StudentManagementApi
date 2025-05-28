using FastEndpoints;
using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Classes.Endpoints;


public class GetClassAverage : Endpoint<GetClassAverageRequest, ClassAverageResponse>
{

    public override void Configure()
    {
        Get("/api/classes/{ClassId}/average");
        AllowAnonymous();
       
    }

    public override async Task HandleAsync(GetClassAverageRequest req, CancellationToken ct)
    {
        try
        {
            IClassService? classService = TryResolve<IClassService>();
            // Check if class exists
            var classModel = await classService.GetByIdAsync(req.ClassId);
            if (classModel == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            // Get average mark
            var response = await classService.GetAverageMarksAsync(req.ClassId , classModel);
            if (response == null)
            {
                await SendNotFoundAsync(ct);
                return ;
            }
            await SendOkAsync(response, ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}