using TutorDesk.Domain.Modules.Auth.Entities;


namespace TutorDesk.Application.Modules.Auth.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByEmail(string email);
        Task InsertUser(User user);
    }
}