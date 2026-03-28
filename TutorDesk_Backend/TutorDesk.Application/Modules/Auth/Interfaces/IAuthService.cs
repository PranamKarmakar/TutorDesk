using TutorDesk.Application.Modules.Auth.Dtos;

public interface IAuthService
{
    Task Register(RegisterDto req);
    Task<AuthResponseDto> Login(LoginDto req);
}