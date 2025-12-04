using FluentValidation;
using WorkSchedulingSystem.Application.DTO.Job;

namespace WorkSchedulingSystem.Application.Validations.JobModelValidation;

public class CreateJobValidation : AbstractValidator<CreateJobDto>
{
    public CreateJobValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Job title is required and must be <= 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(x => x.Duration)
            .NotNull()
            .WithMessage("Duration is required");
    }
}
