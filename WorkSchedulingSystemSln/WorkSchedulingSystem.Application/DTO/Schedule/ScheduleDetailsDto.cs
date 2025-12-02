using WorkSchedulingSystem.Domain.Enums;

namespace WorkSchedulingSystem.Application.DTO.Schedule;

public record ScheduleDetailsDto
(
    Guid ScheduleId,
    DateTime Date,
    ScheduleStatus Status,
    List<Job.JobDetailsDto> Jobs
);
