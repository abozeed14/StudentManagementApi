using FastEndpoints;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Students.Endpoints;

public class DeleteStudentRequest
{
    public int Id { get; set; }
}

public class DeleteStudent : Endpoint<DeleteStudentRequest>
{

    public override void Configure()
    {
        Delete("/api/student/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteStudentRequest req, CancellationToken ct)
    {
        try
        {
            IStudentService? studentService = TryResolve<IStudentService>();

            var result = await studentService.DeleteAsync(req.Id);

            if (!result)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendNoContentAsync(ct);
        }
        catch (Exception ex)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}