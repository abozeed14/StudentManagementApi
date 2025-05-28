using FastEndpoints;
using StudentManagement.API.Features.Enrollments.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Enrollments.Endpoints;

public class EnrollStudent : Endpoint<EnrollmentRequest, EnrollmentResponse>
{
  
    public override void Configure()
    {
        Post("/api/enrollments");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EnrollmentRequest req, CancellationToken ct)
    {
        try
        {
            IEnrollmentService? enrollmentService = TryResolve<IEnrollmentService>();
            // Enroll student
            var response = await enrollmentService.EnrollStudentAsync(req);

            await SendAsync(response);
        }
        catch (KeyNotFoundException ex)
        {
            await SendNotFoundAsync(ct);
        }
        catch (InvalidOperationException ex)
        {
            await SendErrorsAsync(400, ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}