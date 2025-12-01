using Microsoft.AspNetCore.Identity;
using WorkSchedulingSystem.Domain.Enums;
using WorkSchedulingSystem.Domain.Exceptions;

namespace WorkSchedulingSystem.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Gender Gender { get; private set; }
    public DateTime BirthDate { get; private set; }

    public User()
    {

    }

    public static User Create(
         string firstName,
         string lastName,
         Gender gender,
         string email,
         DateTime birthDate)
    {

        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainValidationException("First name is required");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainValidationException("Last name is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainValidationException("Email is required");

        if (birthDate >= DateTime.UtcNow)
            throw new DomainValidationException("Birth date must be in the past");

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            Email = email,
            BirthDate = birthDate,
            UserName = email,
            EmailConfirmed = false
        };

        return user;
    }
}
