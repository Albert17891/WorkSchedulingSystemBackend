namespace WorkSchedulingSystem.Application.DTO.Job;

public record CreateJobDto
(
    string Title,
    string Description,
    TimeSpan Duration
);
