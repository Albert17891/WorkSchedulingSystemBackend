using WorkSchedulingSystem.Application.DTO.Job;

namespace WorkSchedulingSystem.Application.DTO.Schedule;

public record UpdateScheduleDto
(
    Guid ScheduleId,
    DateTime Date,
    List<UpdateJobDto> Jobs
);
