using WorkSchedulingSystem.Application.Common;
using WorkSchedulingSystem.Application.DTO.User;
using WorkSchedulingSystem.Domain.Entities;

namespace WorkSchedulingSystem.Application.ServiceContracts;

public interface IUserService
{
    Task<ApiResult<User>> CreateUserAsync(UserRegisterDto userRegister);
    Task<string> LoginAsync(LoginDto loginDto);
}
