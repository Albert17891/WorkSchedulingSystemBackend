using WorkSchedulingSystem.Application.Common;
using WorkSchedulingSystem.Application.DTO.User;

namespace WorkSchedulingSystem.Application.ServiceContracts;

public interface IUserService
{
    Task<ApiResult<UserResponseDto>> CreateUserAsync(UserRegisterDto userRegister);
    Task<string> LoginAsync(LoginDto loginDto);
}
