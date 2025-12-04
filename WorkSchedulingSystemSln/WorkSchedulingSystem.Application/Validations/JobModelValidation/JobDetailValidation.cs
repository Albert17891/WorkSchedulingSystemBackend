using FluentValidation;
using WorkSchedulingSystem.Application.DTO.Job;

namespace WorkSchedulingSystem.Application.Validations.JobModelValidation;

public class JobDetailsValidation : AbstractValidator<JobDetailsDto>
{
    public JobDetailsValidation()
    {
        RuleFor(x => x.JobId)
            .NotEmpty()
            .WithMessage("JobId is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(150)
            .WithMessage("Job title is required and must not exceed 150 characters.");

        RuleFor(x => x.Description)
          .NotEmpty()
          .WithMessage("Description is required.");

        RuleFor(x => x.Duration)
            .NotNull()
            .WithMessage("Duration is required.");
    }
}