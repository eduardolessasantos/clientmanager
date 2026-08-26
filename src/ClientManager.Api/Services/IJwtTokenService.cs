using ClientManager.Api.DTOs;

namespace ClientManager.Api.Services;

public interface IJwtTokenService
{
    TokenResultDto GenerateToken(string email, string role = "User");
}
