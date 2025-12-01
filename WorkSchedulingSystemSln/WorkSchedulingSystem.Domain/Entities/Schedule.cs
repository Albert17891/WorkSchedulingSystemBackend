using WorkSchedulingSystem.Domain.Enums;

namespace WorkSchedulingSystem.Domain.Entities;

public class Schedule
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime Date { get; private set; }
    public ScheduleStatus Status { get; private set; }
    public List<Job> Jobs { get; private set; }
    private Schedule()
    {
        Id = Guid.NewGuid();
        Jobs = new List<Job>();
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
        Jobs.Add(job);
    }
}
