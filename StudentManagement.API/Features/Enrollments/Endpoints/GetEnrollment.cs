using FastEndpoints;
using StudentManagement.API.Features.Enrollments.DTOs;
using StudentManagement.API.Services.Interfaces;

namespace StudentManagement.API.Features.Enrollments.Endpoints;

public class GetEnrollmentRequest
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
}

public class GetEnrollment : Endpoint<GetEnrollmentRequest, EnrollmentResponse>
{
    
    public override void Configure()
    {
        Get("/api/enrollments/{StudentId}/{ClassId}");
        AllowAnonymous();
       
    }

    public override async Task HandleAsync(GetEnrollmentRequest req, CancellationToken ct)
    {
        try
        {
            IEnrollmentService? enrollmentService = TryResolve<IEnrollmentService>();
            var enrollment = await enrollmentService.GetEnrollmentAsync(req);

            if (enrollment == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var response = new EnrollmentResponse
            {
                StudentId = enrollment.StudentId,
                ClassId = enrollment.ClassId,
                EnrolledAt = enrollment.EnrolledAt
            };

            await SendOkAsync(response, ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(400, ct);
        }
    }
}