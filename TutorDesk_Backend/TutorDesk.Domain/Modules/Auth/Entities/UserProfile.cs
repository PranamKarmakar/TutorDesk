namespace TutorDesk.Domain.Modules.Auth.Entities;

public class UserProfile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string? Summary { get; set; }
    public int ExperienceYears { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
