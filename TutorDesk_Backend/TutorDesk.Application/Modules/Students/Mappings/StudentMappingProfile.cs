using AutoMapper;
using TutorDesk.Domain.Modules.Students.Entities;
using TutorDesk.Application.Modules.Students.Dtos;

namespace TutorDesk.Application.Modules.Students.Mappings;

public class StudentMappingProfile : Profile
{
    public StudentMappingProfile()
    {
        CreateMap<CreateStudentDto, Student>();

        CreateMap<Student, StudentResponseDto>();
    }
}