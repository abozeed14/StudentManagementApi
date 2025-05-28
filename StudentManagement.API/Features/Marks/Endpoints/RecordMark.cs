using FastEndpoints;
using StudentManagement.API.Features.Marks.DTOs;
using StudentManagement.API.Models;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Marks.Endpoints;

public class RecordMark : Endpoint<MarkRequest, MarkResponse>
{
    private readonly IMarkService _markService;

    public RecordMark(IMarkService markService)
    {
        _markService = markService;
    }

    public override void Configure()
    {
        Post("/api/marks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MarkRequest req, CancellationToken ct)
    {
        try
        {
            // Map request to domain model
            var mark = new Mark
            {
                StudentId = req.StudentId,
                ClassId = req.ClassId,
                ExamMark = req.ExamMark,
                AssignmentMark = req.AssignmentMark
            };

            // Record mark
            var recordedMark = await _markService.RecordMarkAsync(req);


            await SendAsync(recordedMark);
        }
        catch (KeyNotFoundException ex)
        {
            await SendNotFoundAsync(ct);
        }
        catch (InvalidOperationException ex)
        {
            await SendErrorsAsync(400, ct);
        }
        catch (Exception ex)
        {
            await SendErrorsAsync(500, ct);
        }
    }
}