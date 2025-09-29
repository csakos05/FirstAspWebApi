using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace FirstAsp.Services;

public class AuthenticationService
{
    private readonly IConfiguration _configuration;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateTestToken()
    {
        // Get JWT settings
        var secretKey = _configuration["JwtSettings:SecretKey"] ?? "defaultsecretkey1234567890abcdefgh";
        var issuer = _configuration["JwtSettings:Issuer"] ?? "DefaultIssuer";
        var audience = _configuration["JwtSettings:Audience"] ?? "DefaultAudience";
        
        // Create security key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        // Create signing credentials
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        // Create claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "testuser"),
            new Claim(JwtRegisteredClaimNames.Name, "Test User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "TestRole")
        };
        
        // Create token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );
        
        // Return serialized token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}