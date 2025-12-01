using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkSchedulingSystem.Domain.Entities;
using WorkSchedulingSystem.Domain.RepositoryContracts;
using WorkSchedulingSystem.Infrastrucuture.DataContext;
using WorkSchedulingSystem.Infrastrucuture.Repositories;

namespace WorkSchedulingSystem.Infrastrucuture;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!));

        services.AddIdentityCore<User>()
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
}
