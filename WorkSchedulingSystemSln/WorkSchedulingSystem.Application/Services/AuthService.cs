using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkSchedulingSystem.Application.Common.Settings;
using WorkSchedulingSystem.Application.ServiceContracts;
using WorkSchedulingSystem.Domain.Entities;

namespace WorkSchedulingSystem.Application.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    public AuthService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> GenerateJwtTokenAsync(User user, UserManager<User> userManager)
    {
        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
         {
           new Claim(JwtRegisteredClaimNames.Sub, user.Email),
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
           new Claim(ClaimTypes.NameIdentifier, user.Id),
           new Claim(ClaimTypes.Name, user.UserName)
         };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
