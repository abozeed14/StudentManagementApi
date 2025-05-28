using FastEndpoints;
using StudentManagement.API.Features.Marks.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Marks.Endpoints;



public class GetMark : Endpoint<GetMarkRequest, MarkResponse>
{
    private readonly IMarkService _markService;

    public GetMark(IMarkService markService)
    {
        _markService = markService;
    }

    public override void Configure()
    {
        Get("/api/marks/{StudentId}/{ClassId}");
        AllowAnonymous();
       
    }

    public override async Task HandleAsync(GetMarkRequest req, CancellationToken ct)
    {
        try
        {
            var response = await _markService.GetMarkAsync(req);

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