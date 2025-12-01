using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkSchedulingSystem.Domain.Entities;

namespace WorkSchedulingSystem.Infrastrucuture.DataContext;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> opt): base(opt)
    {
        
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
