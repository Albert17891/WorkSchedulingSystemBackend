using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WorkSchedulingSystem.Application.Common.Settings;
using WorkSchedulingSystem.Application.ServiceContracts;
using WorkSchedulingSystem.Application.Services;

namespace WorkSchedulingSystem.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IScheduleService, ScheduleService>();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer(options =>
         {
             var jwt = configuration.GetSection("JwtSettings").Get<JwtSettings>();

             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,

                 ValidIssuer = jwt.Issuer,
                 ValidAudience = jwt.Audience,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),

                 ClockSkew = TimeSpan.Zero,

                 RoleClaimType = ClaimTypes.Role,
                 NameClaimType = ClaimTypes.Name
             };

             options.Events = new JwtBearerEvents
             {
                 OnAuthenticationFailed = context =>
                 {
                     Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                     return Task.CompletedTask;
                 },
                 OnTokenValidated = context =>
                 {
                     Console.WriteLine("Token validated successfully");
                     return Task.CompletedTask;
                 },
                 OnChallenge = context =>
                 {
                     Console.WriteLine($"OnChallenge error: {context.Error}, {context.ErrorDescription}");
                     return Task.CompletedTask;
                 }
             };
         });

        return services;
    }
}
