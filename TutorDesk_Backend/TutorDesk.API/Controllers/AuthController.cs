using Microsoft.AspNetCore.Mvc;
using TutorDesk.Application.Modules.Auth.Dtos;
using TutorDesk.Application.Modules.Auth.Interfaces;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto req)
    {
        await _authService.Register(req);
        return Ok(new { message = "Registration successful" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto req)
    {
        var response = await _authService.Login(req);
        return Ok(response);
    }
}