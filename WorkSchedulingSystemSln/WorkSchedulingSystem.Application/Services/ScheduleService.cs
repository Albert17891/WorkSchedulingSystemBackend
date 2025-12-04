using WorkSchedulingSystem.Application.Common;
using WorkSchedulingSystem.Application.DTO.Job;
using WorkSchedulingSystem.Application.DTO.Schedule;
using WorkSchedulingSystem.Application.ServiceContracts;
using WorkSchedulingSystem.Domain.Entities;
using WorkSchedulingSystem.Domain.Enums;
using WorkSchedulingSystem.Domain.RepositoryContracts;

namespace WorkSchedulingSystem.Application.Services;

public class ScheduleService : IScheduleService
{
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResult<object>> ChangeScheduleStatus(Guid scheduleId, ScheduleStatus status)
    {
        var schedule = await _unitOfWork.Schedules.GetScheduleByIdAsync(scheduleId);

        if (schedule == null)
        {
            return ApiResult<object>.Failure("Schedule not found.");
        }

        switch (status)
        {
            case ScheduleStatus.Approved:
                schedule.Approve();
                break;
            case ScheduleStatus.Rejected:
                schedule.Reject();
                break;
            default:
                return ApiResult<object>.Failure("Invalid schedule status.");
        }

        await _unitOfWork.Schedules.UpdateScheduleAsync(schedule);
        await _unitOfWork.CompleteAsync();

        return ApiResult<object>.Success(null);
    }

    public async Task<ApiResult<object>> CreateScheduleAsync(CreateScheduleDto createScheduleDto, Guid userId)
    {
        var schedule = Schedule.Create(userId, createScheduleDto.Date);

        foreach (var jobDto in createScheduleDto.Jobs)
        {
            var job = Job.Create(jobDto.Title, jobDto.Description, jobDto.Duration);
            schedule.AddJob(job);
        }

        await _unitOfWork.Schedules.AddScheduleAsync(schedule);

        await _unitOfWork.CompleteAsync();

        return ApiResult<object>.Success(schedule);
    }

    public async Task<ApiResult<object>> DeleteScheduleAsync(Guid scheduleId)
    {
        await _unitOfWork.Schedules.DeleteScheduleAsync(scheduleId);

        return ApiResult<object>.Success(null);
    }

    public async Task<ApiResult<IEnumerable<ScheduleDetailsDto>>> GetAllScheduleAsync()
    {
        var schedules = await _unitOfWork.Schedules.GetAllSchedulesAsync();

        if (schedules == null || !schedules.Any())
        {
            return ApiResult<IEnumerable<ScheduleDetailsDto>>.Failure("No schedules found.");
        }

        var scheduleDetailsDtos = schedules.Select(schedule => new ScheduleDetailsDto(
              ScheduleId: schedule.Id,
              Date: schedule.Date,
              Status: schedule.Status,
              Jobs: schedule.Jobs?.Select(job => new JobDetailsDto(
                    JobId: job.Id,
                    Title: job.Title,
                    Description: job.Description ?? string.Empty,
                    Duration: job.Duration
              )).ToList() ?? new List<JobDetailsDto>()
           )).ToList();

        return ApiResult<IEnumerable<ScheduleDetailsDto>>.Success(scheduleDetailsDtos);
    }

    public async Task<ApiResult<IEnumerable<ScheduleDetailsDto>>> GetPendingSchedulesAsync()
    {
        var schedules = await _unitOfWork.Schedules.GetPendingSchedulesAsync();

        if (schedules == null || !schedules.Any())
        {
            return ApiResult<IEnumerable<ScheduleDetailsDto>>.Failure("No pending schedules found.");
        }

        var scheduleDetailsDtos = schedules.Select(schedule => new ScheduleDetailsDto(
              ScheduleId: schedule.Id,
              Date: schedule.Date,
              Status: schedule.Status,
              Jobs: schedule.Jobs?.Select(job => new JobDetailsDto(
                    JobId: job.Id,
                    Title: job.Title,
                    Description: job.Description ?? string.Empty,
                    Duration: job.Duration
              )).ToList() ?? new List<JobDetailsDto>()
           )).ToList();

        return ApiResult<IEnumerable<ScheduleDetailsDto>>.Success(scheduleDetailsDtos);
    }

    public async Task<ApiResult<IEnumerable<ScheduleDetailsDto>>> GetSchedulesByUserIdAsync(Guid userId)
    {
        var schedules = await _unitOfWork.Schedules.GetSchedulesByUserIdAsync(userId);

        if (schedules == null)
        {
            return ApiResult<IEnumerable<ScheduleDetailsDto>>.Failure("No schedules found for the user.");
        }

        var scheduleDetailsDtos = schedules.Select(schedule => new ScheduleDetailsDto(
              ScheduleId: schedule.Id,
              Date: schedule.Date,
              Status: schedule.Status,
              Jobs: schedule.Jobs?.Select(job => new JobDetailsDto(
                    JobId: job.Id,
                    Title: job.Title,
                    Description: job.Description ?? string.Empty,
                    Duration: job.Duration
              )).ToList() ?? new List<JobDetailsDto>()
           )).ToList();

        return ApiResult<IEnumerable<ScheduleDetailsDto>>.Success(scheduleDetailsDtos);
    }

    public async Task<ApiResult<object>> UpdateScheduleAsync(UpdateScheduleDto updateScheduleDto, Guid userId)
    {
        var schedule = Schedule.Create(userId, updateScheduleDto.Date);

        foreach (var jobDto in updateScheduleDto.Jobs)
        {
            var job = Job.Create(jobDto.Title, jobDto.Description, jobDto.Duration);
            schedule.AddJob(job);
        }

        await _unitOfWork.Schedules.UpdateScheduleAsync(schedule);

        return ApiResult<object>.Success(schedule);
    }
}
