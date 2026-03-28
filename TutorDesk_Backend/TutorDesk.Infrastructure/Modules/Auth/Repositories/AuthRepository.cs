using TutorDesk.Infrastructure.Data;
using TutorDesk.Common.Extensions;
using TutorDesk.Domain.Modules.Auth.Entities;
using TutorDesk.Application.Modules.Auth.Interfaces;


namespace TutorDesk.Infrastructure.Modules.Auth.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DbSession _session;

        public AuthRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<User?> GetByEmail(string email)
        {
            using var cmd = _session.CreateCommand("sp_GetUserByEmail");
            cmd.Parameters.AddWithValue("@Email", email);

            var table = await _session.ExecuteAsync(cmd);
            return table.Convert<User>().FirstOrDefault();
        }

        public async Task InsertUser(User user)
        {
            using var cmd = _session.CreateCommand("sp_CreateUser");

            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@TenantId", user.TenantId);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@Role", user.Role);

            await _session.ExecuteNonQueryAsync(cmd);
        }
    }
}