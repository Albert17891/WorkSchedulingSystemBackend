namespace WorkSchedulingSystem.Application.DTO.Job;

public record UpdateJobDto
(
    Guid JobId,
    string Title,
    string Description,
    TimeSpan Duration
);

