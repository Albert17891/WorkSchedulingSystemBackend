using WorkSchedulingSystem.Domain.Entities;

namespace WorkSchedulingSystem.Domain.RepositoryContracts;

public interface IScheduleRepository
{
    Task AddScheduleAsync(Schedule schedule);
    Task DeleteScheduleAsync(Guid scheduleId);
    Task UpdateScheduleAsync(Schedule schedule);
    Task<Schedule?> GetScheduleByIdAsync(Guid scheduleId);
    Task<IEnumerable<Schedule>> GetSchedulesByUserIdAsync(Guid userId);
    Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
    Task<IEnumerable<Schedule?>> GetPendingSchedulesAsync();
}
