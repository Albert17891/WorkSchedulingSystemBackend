using WorkSchedulingSystem.Domain.Enums;

namespace WorkSchedulingSystem.Domain.Entities;

public class Schedule
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime Date { get; private set; }
    public ScheduleStatus Status { get; private set; }
    private readonly List<Job> _jobs = new();
    public IReadOnlyCollection<Job> Jobs => _jobs.AsReadOnly();
    private Schedule()
    {
        Id = Guid.NewGuid();    
    }
    public static Schedule Create(Guid userId, DateTime date)
    {
        return new Schedule
        {
            UserId = userId,
            Date = date,
            Status = ScheduleStatus.Pending
        };
    }

    public void Approve()
    {
        Status = ScheduleStatus.Approved;
    }

    public void Reject()
    {
        Status = ScheduleStatus.Rejected;
    }

    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }
}
