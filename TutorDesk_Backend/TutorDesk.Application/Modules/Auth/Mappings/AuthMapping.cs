using TutorDesk.Application.Modules.Auth.Dtos;
using TutorDesk.Domain.Modules.Auth.Entities;

namespace TutorDesk.Application.Modules.Auth.Mappings
{
    public static class AuthMapping
    {
        public static User ToUser(RegisterDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password
            };
        }
    }
}
