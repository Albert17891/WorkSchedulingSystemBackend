using WorkSchedulingSystem.Application.DTO.Job;

namespace WorkSchedulingSystem.Application.DTO.Schedule;

public record CreateScheduleDto(  
    DateTime Date,
    List<CreateJobDto> Jobs
);

