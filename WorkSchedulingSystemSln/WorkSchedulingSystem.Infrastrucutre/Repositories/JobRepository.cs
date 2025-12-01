using Microsoft.EntityFrameworkCore;
using WorkSchedulingSystem.Domain.Entities;
using WorkSchedulingSystem.Domain.RepositoryContracts;
using WorkSchedulingSystem.Infrastrucutre.DataContext;

namespace WorkSchedulingSystem.Infrastrucutre.Repositories;

public class JobRepository : IJobRepository
{
    private readonly AppDbContext _context;

    public JobRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddJobAsync(Job job)
    {
        await _context.Jobs.AddAsync(job);
    }

    public async Task DeleteJobAsync(Job job)
    {
        var existingJob = await _context.Jobs.FindAsync(job.Id);

        if (existingJob != null)
        {
            throw new InvalidOperationException("Job does not exist.");
        }

        _context.Jobs.Remove(job);
    }

    public async Task<IEnumerable<Job>> GetAllJobsAsync()
    {
        return await _context.Jobs.ToListAsync();
    }

    public async Task<Job?> GetJobByIdAsync(Guid jobId)
    {
        return await _context.Jobs.FirstOrDefaultAsync(j => j.Id == jobId);
    }

    public async Task<IEnumerable<Job>> GetJobsByUserIdAsync(Guid userId)
    {
        return await _context.Jobs
             .Where(j => j.AssignedUserId == userId)
             .ToListAsync();
    }

    public async Task UpdateJobAsync(Job job)
    {
        var existingJob = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == job.Id);

        if (existingJob == null)
        {
            throw new InvalidOperationException("Job does not exist.");
        }
        _context.Jobs.Update(job);
    }
}
