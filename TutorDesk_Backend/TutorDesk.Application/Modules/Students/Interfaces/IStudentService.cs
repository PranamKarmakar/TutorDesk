using TutorDesk.Application.Modules.Students.Dtos;

public interface IStudentService
{
    Task<StudentResponseDto> CreateStudent(Guid tenantId, CreateStudentDto dto);
    Task<List<StudentResponseDto>> GetStudents(Guid tenantId);
}