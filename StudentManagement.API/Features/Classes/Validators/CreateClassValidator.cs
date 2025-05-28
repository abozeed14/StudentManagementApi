using FluentValidation;
using StudentManagement.API.Features.Classes.DTOs;

namespace StudentManagement.API.Features.Classes.Validators;

public class CreateClassValidator : AbstractValidator<CreateClassRequest>
{
    public CreateClassValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Class name is required")
            .MaximumLength(100).WithMessage("Class name cannot exceed 100 characters");

        RuleFor(x => x.Teacher)
            .NotEmpty().WithMessage("Teacher name is required")
            .MaximumLength(100).WithMessage("Teacher name cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");
    }
}