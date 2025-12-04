using FluentValidation;
using WorkSchedulingSystem.Application.DTO.User;

namespace WorkSchedulingSystem.Application.Validations;

public class UserRegisterValidation : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidation()
    {
        RuleFor(x => x.FirstName)
          .NotEmpty().WithMessage("First name is required.")
          .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender selection.");


        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Today).WithMessage("Birth date must be in the past.")
            .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("Birth date is too far in the past.")
            .Must(BeAtLeast18YearsOld).WithMessage("You must be at least 18 years old.");
    }

    private bool BeAtLeast18YearsOld(DateTime birthDate)
    {
        return birthDate <= DateTime.Today.AddYears(-18);
    }
}
