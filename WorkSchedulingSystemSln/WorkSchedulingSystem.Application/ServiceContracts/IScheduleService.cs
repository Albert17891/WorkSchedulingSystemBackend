using WorkSchedulingSystem.Application.Common;
using WorkSchedulingSystem.Application.DTO.Schedule;
using WorkSchedulingSystem.Domain.Enums;

namespace WorkSchedulingSystem.Application.ServiceContracts;

public interface IScheduleService
{
    Task<ApiResult<object>> CreateScheduleAsync(CreateScheduleDto createScheduleDto, Guid userId);
    Task<ApiResult<object>> UpdateScheduleAsync(UpdateScheduleDto updateScheduleDto, Guid userId);
    Task<ApiResult<object>> DeleteScheduleAsync(Guid scheduleId);
    Task<ApiResult<IEnumerable<ScheduleDetailsDto>>> GetPendingSchedulesAsync();
    Task<ApiResult<object>> ChangeScheduleStatus(Guid scheduleId, ScheduleStatus status);
    Task<ApiResult<IEnumerable<ScheduleDetailsDto>>> GetSchedulesByUserIdAsync(Guid userId);
    Task<ApiResult<IEnumerable<ScheduleDetailsDto>>> GetAllScheduleAsync();
}