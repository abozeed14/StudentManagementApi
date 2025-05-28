using FastEndpoints;
using StudentManagement.API.Features.Classes.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Classes.Endpoints;





public class GetAllClasses : Endpoint<GetAllClassesQueryParameter, GetAllClassesResponse>
{
   
    public override void Configure()
    {
        Get("/api/classes");
        AllowAnonymous();
       
    }

    public override async Task HandleAsync(GetAllClassesQueryParameter req, CancellationToken ct)
    {
        try
        {
            IClassService classService = TryResolve<IClassService>();
           
            // Get classes with filtering and pagination
            var classes = await classService.GetAllAsync(req);

            // Map to response
            var response = new GetAllClassesResponse
            {
                Classes = classes.Select(c => new ClassResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Teacher = c.Teacher,
                    Description = c.Description
                }).ToList(),
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