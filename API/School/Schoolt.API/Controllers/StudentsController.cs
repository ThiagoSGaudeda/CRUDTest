using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schoolt.API.Data;
using Schoolt.API.Models;

namespace Schoolt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly StudentDbContext studentDbContext;

        public StudentsController(StudentDbContext studentDbContext)
        {
            this.studentDbContext = studentDbContext;
        }


        //Get ALL Students
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentDbContext.Students.ToListAsync();
            return Ok(students);
        }

        //get a single student
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid id)
        {
            var student = await studentDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if(student != null)
            {
                return Ok(student);
            }
            return NotFound("Student not found");
        }

        //Add a student
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            student.Id = Guid.NewGuid();
            await studentDbContext.Students.AddAsync(student);
            await studentDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        //Update a student
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] Student student)
        {
            var existingStudent = await studentDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if(existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.BirthYear = student.BirthYear;
                existingStudent.SchoolId = student.SchoolId;
                await studentDbContext.SaveChangesAsync();
                return Ok(existingStudent);
            }

            return NotFound("Student not found");
        }

        // Delete a student
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var existingStudent = await studentDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStudent != null)
            {
               studentDbContext.Remove(existingStudent);
               await studentDbContext.SaveChangesAsync();
                return Ok(existingStudent);
            }

            return NotFound("Student not found");
        }
    }
}
