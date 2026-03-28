using TutorDesk.Domain.Modules.Auth.Entities;

namespace TutorDesk.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}