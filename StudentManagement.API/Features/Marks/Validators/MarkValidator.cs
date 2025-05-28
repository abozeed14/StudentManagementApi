using FluentValidation;
using StudentManagement.API.Features.Marks.DTOs;

namespace StudentManagement.API.Features.Marks.Validators;

public class MarkValidator : AbstractValidator<MarkRequest>
{
    public MarkValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Student ID must be greater than 0");

        RuleFor(x => x.ClassId)
            .GreaterThan(0).WithMessage("Class ID must be greater than 0");

        RuleFor(x => x.ExamMark)
            .InclusiveBetween(0, 100).WithMessage("Exam mark must be between 0 and 100");

        RuleFor(x => x.AssignmentMark)
            .InclusiveBetween(0, 100).WithMessage("Assignment mark must be between 0 and 100");
    }
}