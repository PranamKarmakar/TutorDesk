using TutorDesk.Domain.Modules.Bases;

public class User : BaseEntity
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Role { get; set; } = "Tutor";
}