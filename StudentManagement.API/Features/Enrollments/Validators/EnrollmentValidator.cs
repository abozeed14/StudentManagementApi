using FluentValidation;
using StudentManagement.API.Features.Enrollments.DTOs;

namespace StudentManagement.API.Features.Enrollments.Validators;

public class EnrollmentValidator : AbstractValidator<EnrollmentRequest>
{
    public EnrollmentValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Student ID must be greater than 0");

        RuleFor(x => x.ClassId)
            .GreaterThan(0).WithMessage("Class ID must be greater than 0");
    }
}