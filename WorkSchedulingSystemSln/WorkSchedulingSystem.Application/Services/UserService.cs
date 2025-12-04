using Microsoft.AspNetCore.Identity;
using WorkSchedulingSystem.Application.Common;
using WorkSchedulingSystem.Application.DTO.User;
using WorkSchedulingSystem.Application.ServiceContracts;
using WorkSchedulingSystem.Domain.Constants;
using WorkSchedulingSystem.Domain.Entities;

namespace WorkSchedulingSystem.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;

    public UserService(UserManager<User> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }

    public async Task<ApiResult<UserResponseDto>> CreateUserAsync(UserRegisterDto userRegister)
    {
        var user = User.Create(
             userRegister.FirstName,
             userRegister.LastName,
             userRegister.Gender,
             userRegister.Email,
             userRegister.BirthDate.ToUniversalTime());

        var result = await _userManager.CreateAsync(user, userRegister.Password);

        if (!result.Succeeded)
        {
            return ApiResult<UserResponseDto>.Failure("User creation failed");
        }

        await _userManager.AddToRoleAsync(user, Roles.Worker);

        var userResponse=new UserResponseDto(user.FirstName, user.LastName,user.Email);
        

        return ApiResult<UserResponseDto>.Success(userResponse);
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var token = await _authService.GenerateJwtTokenAsync(user, _userManager);

        return token;
    }
}
