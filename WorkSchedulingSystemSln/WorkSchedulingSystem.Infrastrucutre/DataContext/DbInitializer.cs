using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WorkSchedulingSystem.Domain.Constants;
using WorkSchedulingSystem.Domain.Entities;
using WorkSchedulingSystem.Domain.Enums;

namespace WorkSchedulingSystem.Infrastructure.DataContext;

public static class DbInitializer
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        await SeedRole(roleManager, Roles.Admin);
        await SeedRole(roleManager, Roles.Worker);

        var adminEmail = "admin@system.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
             adminUser = User.Create(
                firstName: "System",
                lastName: "Admin",
                gender: Gender.Male,
                email: adminEmail,
                birthDate: DateTime.UtcNow.AddYears(-30)
                );
        }

        var result = await userManager.CreateAsync(adminUser, "Admin@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, Roles.Admin);
        }
    }

    private static async Task SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
