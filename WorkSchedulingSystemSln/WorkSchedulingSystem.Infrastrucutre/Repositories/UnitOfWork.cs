using WorkSchedulingSystem.Domain.RepositoryContracts;
using WorkSchedulingSystem.Infrastrucuture.DataContext;

namespace WorkSchedulingSystem.Infrastrucuture.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IJobRepository _jobRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public UnitOfWork(AppDbContext context,
                      IJobRepository jobRepository,
                      IScheduleRepository scheduleRepository)
    {
        _context = context;
        _jobRepository = jobRepository;
        _scheduleRepository = scheduleRepository;
    }
    public IJobRepository Jobs => _jobRepository;

    public IScheduleRepository Schedules => _scheduleRepository;

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
