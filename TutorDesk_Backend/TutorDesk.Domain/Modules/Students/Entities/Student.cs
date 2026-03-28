using TutorDesk.Domain.Modules.Bases;

namespace TutorDesk.Domain.Modules.Students.Entities;

public class Student : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string ParentPhone { get; set; } = string.Empty;
    public decimal MonthlyFees { get; set; }

   
}