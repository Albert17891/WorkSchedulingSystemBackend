using Microsoft.AspNetCore.Mvc;
using System.Net;
using WorkSchedulingSystem.Application.DTO.User;
using WorkSchedulingSystem.Application.ServiceContracts;
using WorkSchedulingSystem.Domain.Entities;

namespace WorkSchedulingSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        return Ok(user);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _userService.LoginAsync(dto);
        return Ok(new { Token = token });
    }
}
