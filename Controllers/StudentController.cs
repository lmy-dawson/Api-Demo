using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Repositories;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    public StudentController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var students = await _studentRepository.GetAll();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _studentRepository.GetById(id);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        await _studentRepository.Create(student);

        return CreatedAtAction(nameof(GetStudent), new { id = student.ID }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student student)
    {
        if (id != student.ID)
        {
            return BadRequest();
        }

        await _studentRepository.Update(student);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        await _studentRepository.Delete(id);

        return Ok();
    }
}
