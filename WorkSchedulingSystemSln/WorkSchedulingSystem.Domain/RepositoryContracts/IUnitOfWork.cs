namespace WorkSchedulingSystem.Domain.RepositoryContracts;

public interface IUnitOfWork : IDisposable
{
    IJobRepository Jobs { get; }
    IScheduleRepository Schedules { get; }
    Task<int> CompleteAsync();
}
