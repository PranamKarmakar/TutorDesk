using AutoMapper;
using TutorDesk.Domain.Modules.Auth.Entities;
using TutorDesk.Application.Modules.Auth.Dtos;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<User, AuthResponseDto>();
    }
}