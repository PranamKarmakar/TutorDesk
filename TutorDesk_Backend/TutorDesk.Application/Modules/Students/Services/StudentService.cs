using AutoMapper;
using TutorDesk.Application.Modules.Students.Dtos;

using TutorDesk.Common.Exceptions;
using TutorDesk.Domain.Modules.Students.Entities;

namespace TutorDesk.Application.Modules.Students.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<StudentResponseDto> CreateStudent(Guid tenantId, CreateStudentDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new AppException("Student name is required", 400);

        var student = _mapper.Map<Student>(dto);

        student.Id = Guid.NewGuid();
        student.TenantId = tenantId;
        student.IsActive = true;

        await _repo.AddStudent(student);

        return _mapper.Map<StudentResponseDto>(student);
    }

    public async Task<List<StudentResponseDto>> GetStudents(Guid tenantId)
    {
        var students = await _repo.GetStudents(tenantId);

        return _mapper.Map<List<StudentResponseDto>>(students);
    }
}