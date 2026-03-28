using AutoMapper;
using System.Xml.Linq;
using TutorDesk.Application.Common.Interfaces;
using TutorDesk.Application.Modules.Auth.Dtos;
using TutorDesk.Application.Modules.Auth.Interfaces;
using TutorDesk.Application.Modules.Auth.Mappings;
using TutorDesk.Common.Exceptions;
using TutorDesk.Common.Helpers;

using TutorDesk.Domain.Modules.Auth.Entities;


namespace TutorDesk.Application.Modules.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository repo, IJwtService jwtService, IMapper mapper)
        {
            _repo = repo;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        

        public async Task Register(RegisterDto req)
        {
            var existing = await _repo.GetByEmail(req.Email);

            if (existing != null)
                throw new AppException("User already exists", 400);

            var tenantId = Guid.NewGuid();

            var user = new User
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = req.Name,
                Email = req.Email,
                Role = "Tutor",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            user.PasswordHash = HashHelper.Hash(req.Password);

            await _repo.InsertUser(user);
        }

        public async Task<AuthResponseDto> Login(LoginDto req)
        {
            var user = await _repo.GetByEmail(req.Email);

            if (user == null)
                throw new AppException("Invalid credentials", 401);

            if (user.PasswordHash != HashHelper.Hash(req.Password))
                throw new AppException("Invalid credentials", 401);

            var token = _jwtService.GenerateToken(user);

            var response = _mapper.Map<AuthResponseDto>(user);
            response.Token = token;

            return response;
        }

    }
}
