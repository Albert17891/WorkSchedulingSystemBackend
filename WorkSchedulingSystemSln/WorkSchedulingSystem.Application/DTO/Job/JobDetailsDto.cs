namespace WorkSchedulingSystem.Application.DTO.Job;

public record JobDetailsDto
(
    Guid JobId,
    string Title,
    string Description,
    TimeSpan Duration
);
