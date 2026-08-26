using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClientManager.Api.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace ClientManager.Api.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenResultDto GenerateToken(string email, string role = "User")
    {
        var secretKey = _configuration["JwtSettings:SecretKey"] ?? "ClientManager_Secret_JWT_Key_2026_Super_Secure_Key!";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddHours(8);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"] ?? "ClientManagerApi",
            audience: _configuration["JwtSettings:Audience"] ?? "ClientManagerWeb",
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new TokenResultDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expires,
            UsuarioEmail = email
        };
    }
}
