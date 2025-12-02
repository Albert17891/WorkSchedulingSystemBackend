using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkSchedulingSystem.Application.DTO.Schedule;
using WorkSchedulingSystem.Application.ServiceContracts;
using WorkSchedulingSystem.Domain.Constants;
using WorkSchedulingSystem.Domain.Enums;

namespace WorkSchedulingSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet("GetAllSchedules")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> GetAllSchedules()
    {     
        var result = await _scheduleService.GetAllScheduleAsync();


        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpGet("GetSchedulesByUserId")]
    [Authorize(Roles = Roles.Worker)]
    public async Task<IActionResult> GetSchedulesByUserId()
    {
        var userId = GetCurrentUserId();

        var result = await _scheduleService.GetSchedulesByUserIdAsync(userId.Value);
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost("CreateSchedule")]
    [Authorize(Roles = Roles.Worker)]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleDto dto)
    {
        var userId = GetCurrentUserId();

        var result = await _scheduleService.CreateScheduleAsync(dto, userId.Value);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPut("UpdateSchedule")]
    [Authorize(Roles = Roles.Worker)]
    public async Task<IActionResult> UpdateSchedule([FromBody] UpdateScheduleDto dto)
    {
        var userId = GetCurrentUserId();

        var result = await _scheduleService.UpdateScheduleAsync(dto, userId.Value);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpDelete("{scheduleId:guid}")]
    [Authorize(Roles = Roles.Admin + "," + Roles.Worker)]
    public async Task<IActionResult> DeleteSchedule(Guid scheduleId)
    {
        var result = await _scheduleService.DeleteScheduleAsync(scheduleId);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }


    [HttpGet("pending")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> GetPendingSchedules()
    {
        var result = await _scheduleService.GetPendingSchedulesAsync();

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPatch("{scheduleId:guid}/status")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> ChangeStatus(Guid scheduleId, [FromBody] ScheduleStatus status)
    {
        var result = await _scheduleService.ChangeScheduleStatus(scheduleId, status);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    private Guid? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                          ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return Guid.TryParse(userIdClaim, out var id) ? id : null;
    }
}
