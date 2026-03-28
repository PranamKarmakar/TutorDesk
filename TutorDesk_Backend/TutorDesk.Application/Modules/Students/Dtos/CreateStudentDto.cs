using System;
using System.Collections.Generic;
using System.Text;

namespace TutorDesk.Application.Modules.Students.Dtos
{
    
    public class CreateStudentDto
    {
        public string Name { get; set; } = "";
        public string Class { get; set; } = "";
        public string Subject { get; set; } = "";
        public string ParentPhone { get; set; } = "";
        public decimal MonthlyFees { get; set; }
    }
}
