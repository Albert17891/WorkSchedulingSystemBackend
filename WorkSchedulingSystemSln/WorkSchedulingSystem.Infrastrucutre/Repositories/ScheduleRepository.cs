using Microsoft.EntityFrameworkCore;
using WorkSchedulingSystem.Domain.Entities;
using WorkSchedulingSystem.Domain.Enums;
using WorkSchedulingSystem.Domain.RepositoryContracts;
using WorkSchedulingSystem.Infrastrucuture.DataContext;

namespace WorkSchedulingSystem.Infrastrucuture.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbContext _context;

    public ScheduleRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddScheduleAsync(Schedule schedule)
    {
        await _context.Schedules.AddAsync(schedule);
    }

    public async Task DeleteScheduleAsync(Guid scheduleId)
    {
        var entity = await _context.Schedules.FindAsync(scheduleId);

        if (entity == null)
        {
            throw new InvalidOperationException("Schedule not found.");
        }

        _context.Schedules.Remove(entity);
    }

    public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
    {
        return await _context.Schedules.ToListAsync();
    }

    public async Task<IEnumerable<Schedule?>> GetPendingSchedulesAsync()
    {
        return await _context.Schedules
            .Where(s => s.Status == ScheduleStatus.Pending)
            .Include(x => x.Jobs)
            .ToListAsync();
    }

    public async Task<Schedule?> GetScheduleByIdAsync(Guid scheduleId)
    {
        return await _context.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleId);
    }

    public async Task<IEnumerable<Schedule>> GetSchedulesByUserIdAsync(Guid userId)
    {
        return await _context.Schedules
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateScheduleAsync(Schedule schedule)
    {
        var entity = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == schedule.Id);

        if (entity == null)
        {
            throw new InvalidOperationException("Schedule not found.");
        }

        _context.Schedules.Remove(entity);
    }
}
