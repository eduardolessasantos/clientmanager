using ClientManager.Api.DTOs;
using ClientManager.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    /// <summary>
    /// Autentica um usuário e gera o token JWT Bearer.
    /// </summary>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
        {
            return BadRequest(new { message = "Email e Senha são obrigatórios." });
        }

        // Demo authentication accepting any credentials for testing
        var token = _jwtTokenService.GenerateToken(login.Email, "Admin");
        return Ok(token);
    }
}
