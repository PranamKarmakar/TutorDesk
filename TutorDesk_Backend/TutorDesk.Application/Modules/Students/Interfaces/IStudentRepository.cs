using TutorDesk.Domain.Modules.Students.Entities;

public interface IStudentRepository
{
    Task AddStudent(Student student);
    Task<List<Student>> GetStudents(Guid tenantId);
}