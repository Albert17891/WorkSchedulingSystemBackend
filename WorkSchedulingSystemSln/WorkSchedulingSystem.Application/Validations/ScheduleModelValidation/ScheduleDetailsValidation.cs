using FluentValidation;
using WorkSchedulingSystem.Application.DTO.Schedule;
using WorkSchedulingSystem.Application.Validations.JobModelValidation;

namespace WorkSchedulingSystem.Application.Validations.ScheduleModelValidation;

internal class ScheduleDetailsValidation : AbstractValidator<ScheduleDetailsDto>
{
    public ScheduleDetailsValidation()
    {
        RuleFor(x => x.ScheduleId)
            .NotEmpty()
            .WithMessage("ScheduleId is required.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .Must(date => date != default)
            .WithMessage("Valid schedule date is required.")
            .Must(date => date.Date <= DateTime.Today.AddYears(2))
            .WithMessage("Schedule date cannot be more than 2 years in the future.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid schedule status.");

        RuleFor(x => x.Jobs)
            .NotNull()
            .WithMessage("Jobs list cannot be null.")
            .NotEmpty()
            .WithMessage("Schedule must contain at least one job.")
            .Must(jobs => jobs.Count <= 100)
            .WithMessage("Maximum 100 jobs allowed per schedule.");

        RuleForEach(x => x.Jobs)
            .SetValidator(new JobDetailsValidation());

    }
}
