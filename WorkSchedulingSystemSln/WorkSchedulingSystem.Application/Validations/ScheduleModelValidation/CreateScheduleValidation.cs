using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WorkSchedulingSystem.Application.DTO.Schedule;
using WorkSchedulingSystem.Application.Validations.JobModelValidation;

namespace WorkSchedulingSystem.Application.Validations.ScheduleModelValidation;

public class CreateScheduleValidation : AbstractValidator<CreateScheduleDto>
{
    public CreateScheduleValidation()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Schedule date is required.")
            .Must(BeAValidDate)
            .WithMessage("Schedule date cannot be a default DateTime value.");

        RuleFor(x => x.Jobs)
            .NotNull()
            .WithMessage("Jobs list cannot be null.")
            .NotEmpty()
            .WithMessage("At least one job must be provided.")
            .Must(jobs => jobs.Count <= 50)
            .WithMessage("Maximum 50 jobs allowed per schedule.");

       
        RuleForEach(x => x.Jobs).SetValidator(new CreateJobValidation());        
    }

    private static bool BeAValidDate(DateTime date)
        => date != default && date >= DateTime.Today.AddDays(-1);
}
