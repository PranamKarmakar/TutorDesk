using System;
using System.Collections.Generic;
using System.Text;

namespace TutorDesk.Application.Modules.Auth.Dtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    
}
}
