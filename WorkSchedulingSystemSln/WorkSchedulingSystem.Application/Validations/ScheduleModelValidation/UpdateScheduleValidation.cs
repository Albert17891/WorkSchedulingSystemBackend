using FluentValidation;
using WorkSchedulingSystem.Application.DTO.Schedule;
using WorkSchedulingSystem.Application.Validations.JobModelValidation;

namespace WorkSchedulingSystem.Application.Validations.ScheduleModelValidation;

public class UpdateScheduleValidation : AbstractValidator<UpdateScheduleDto>
{
    public UpdateScheduleValidation()
    {
        RuleFor(x => x.ScheduleId)
            .NotEmpty()
            .WithMessage("ScheduleId is required for update.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .Must(date => date != default)
            .WithMessage("Valid schedule date is required.")
            .Must(date => date >= DateTime.Today.AddDays(-1))
            .WithMessage("Schedule date cannot be too far in the past.");

        When(x => x.Jobs != null && x.Jobs.Any(), () =>
        {
            RuleFor(x => x.Jobs!)
                .NotEmpty()
                .WithMessage("Jobs list cannot be empty if provided.")
                .Must(jobs => jobs.Count <= 50)
                .WithMessage("Maximum 50 jobs allowed per schedule.");


            RuleForEach(x => x.Jobs!).SetValidator(new UpdateJobValidation());
        });
    }
}