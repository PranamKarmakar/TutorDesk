using System.ComponentModel.DataAnnotations;

namespace TutorDesk.Application.Modules.Auth.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = "";
    }
}