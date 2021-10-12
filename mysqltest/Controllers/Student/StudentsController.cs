using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using mysqltest.Controllers;
using mysqltest.Mapping.DTO;
using mysqltest.Models;
using mysqltest.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsCore.Controllers
{
    public class StudentsController : ApiControllerBase
    {
        public StudentsController(ClubsContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        /// <summary>
        /// GetAll Students With Filters
        /// </summary>
        [HttpGet]
        public ActionResult GetStudents([FromQuery] QueryStudentParameters queryparameters)
        {
            var studentsQuery = _context.Students
                                     .OrderBy(c => c.Id) //ordering all students by Id
                                     .AsQueryable(); //To apply filters

            //Applying filters:

            if (queryparameters.FirstName != null)
                studentsQuery = studentsQuery.Where(n => n.FirstName == queryparameters.FirstName); //Checking if the entered FirstName is in the context if FirstName entered

            if (queryparameters.LastName != null)
                studentsQuery = studentsQuery.Where(l => l.LastName == queryparameters.LastName); //Checking if the entered LastName is in the context if LastName entered

            if (queryparameters.Type != null)
                studentsQuery = studentsQuery.Where(t => queryparameters.Type.Contains(t.Type)); //Checking for the student Type in context if entered

            var students = Paginate<StudentDTO>(studentsQuery, queryparameters); //using Paginate with Parameters we have alredy set

            return Ok(students);
        }

        /// <summary>
        /// Get Student By Id
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult GetStudent(int id)
        {
            var student = _context.Students
                               .Where(x => x.Id == id) //searching for Student using Id
                               .ProjectTo<StudentDTO>(_mapper.ConfigurationProvider) //using mapper and StudentDTO
                               .FirstOrDefault(); //Selecting the club by Id with StudentDTO parameters or make it as a default

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        /// <summary>
        /// Post Student
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostStudentAsync(Student studentPost)
        {
            var post_student = _context.Students
                                    .Add(studentPost); //using Add function to post a student

            await _context.SaveChangesAsync(); //saving edit in a database

            return Ok(studentPost);
        }

        /// <summary>
        /// Delete Student
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Students
                                     .FindAsync(id); //searching for student using Id
            if (student == null)
                return NotFound();

            _context.Students.Remove(student); //using Remove function

            await _context.SaveChangesAsync(); //saving all in database

            return student;
        }

        /// <summary>
        /// Patch the Student
        /// </summary>
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Student> value)
        {
            try
            {
                var result = _context.Students.FirstOrDefault(n => n.Id == id); //Getting Student by Id

                if (result == null)
                    return NotFound();

                value.ApplyTo(result, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState); //result gets the values from the patch request

                _context.SaveChanges(); //Saving in database

                if (false == ModelState.IsValid)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex); //Catching exceptions
            }
        }
    }
}