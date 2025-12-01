using WorkSchedulingSystem.Domain.Entities;

namespace WorkSchedulingSystem.Domain.RepositoryContracts;

public interface IJobRepository
{
    Task AddJobAsync(Job job);
    Task DeleteJobAsync(Job job);
    Task UpdateJobAsync(Job job);
    Task<Job?> GetJobByIdAsync(Guid jobId);
    Task<IEnumerable<Job>> GetJobsByUserIdAsync(Guid userId);
    Task<IEnumerable<Job>> GetAllJobsAsync();
}
