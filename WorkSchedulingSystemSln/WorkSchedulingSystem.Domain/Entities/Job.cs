using WorkSchedulingSystem.Domain.Exceptions;

namespace WorkSchedulingSystem.Domain.Entities;

public class Job
{
    public Guid Id { get; private set; }
    public Guid ScheduleId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TimeSpan Duration { get; private set; }

    private Job()
    {
        Id = Guid.NewGuid();
    }

    public static Job Create(string title, string description, TimeSpan duration)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new DomainValidationException("Job title cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(description))
        {
            throw new DomainValidationException("Job description cannot be null or empty.");
        }

        if (duration <= TimeSpan.Zero)
        {
            throw new DomainValidationException("Job duration must be greater than zero.");
        }

        return new Job
        {
            Title = title,
            Description = description,
            Duration = duration
        };
    }
}
