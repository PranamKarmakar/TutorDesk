using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorDesk.Application.Modules.Students.Dtos;

[Authorize]
[ApiController]
[Route("api/students")]
public class StudentsController : BaseController
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(CreateStudentDto dto)
    {
        var student = await _service.CreateStudent(GetTenantId(), dto);
        return Ok("Student created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _service.GetStudents(GetTenantId());
        return Ok(students);
    }
}