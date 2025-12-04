using Microsoft.AspNetCore.Identity;
using WorkSchedulingSystem.Domain.Entities;

namespace WorkSchedulingSystem.Application.ServiceContracts;

public interface IAuthService
{
    Task<string> GenerateJwtTokenAsync(User user, UserManager<User> userManager);
}
