using WorkSchedulingSystem.Domain.Enums;

namespace WorkSchedulingSystem.Application.DTO.User;

public record UserRegisterDto(
    string FirstName,
    string LastName,
    Gender Gender,
    string Email,
    string Password,   
    DateTime BirthDate);
