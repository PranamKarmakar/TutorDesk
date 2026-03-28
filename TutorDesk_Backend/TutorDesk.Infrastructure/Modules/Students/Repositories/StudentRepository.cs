
using TutorDesk.Common.Exceptions;
using TutorDesk.Common.Extensions;

using TutorDesk.Domain.Modules.Students.Entities;
using TutorDesk.Infrastructure.Data;

namespace TutorDesk.Infrastructure.Modules.Students.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly DbSession _session;

    public StudentRepository(DbSession session)
    {
        _session = session;
    }

    public async Task AddStudent(Student student)
    {
        using var cmd = _session.CreateCommand("sp_AddStudent");

        cmd.Parameters.AddWithValue("@Id", student.Id);
        cmd.Parameters.AddWithValue("@TenantId", student.TenantId);
        cmd.Parameters.AddWithValue("@Name", student.Name);
        cmd.Parameters.AddWithValue("@Class", student.Class);
        cmd.Parameters.AddWithValue("@Subject", student.Subject);
        cmd.Parameters.AddWithValue("@ParentPhone", student.ParentPhone);
        cmd.Parameters.AddWithValue("@MonthlyFees", student.MonthlyFees);

        await _session.ExecuteNonQueryAsync(cmd);
    }

    public async Task<List<Student>> GetStudents(Guid tenantId)
    {
        using var cmd = _session.CreateCommand("sp_GetStudentsByTenant");
        cmd.Parameters.AddWithValue("@TenantId", tenantId);

        var table = await _session.ExecuteAsync(cmd);
        return table.Convert<Student>();
    }
}