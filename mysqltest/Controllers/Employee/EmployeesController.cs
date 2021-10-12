using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using mysqltest.Mapping.DTO;
using mysqltest.Models;
using mysqltest.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace mysqltest.Controllers
{
    public class EmployeesController : ApiControllerBase
    {
        public EmployeesController(ClubsContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        // GET
        [HttpGet]
        public ActionResult GetEmployee([FromQuery] QueryEmployeeParameters queryparameters)
        {
            var employeeQuery = _context.Employee
                                        .OrderBy(x => x.Id)
                                        .AsQueryable();

            if (queryparameters.Name != null)
                employeeQuery = employeeQuery.Where(x => x.FirstName.Contains(queryparameters.Name));

            if (queryparameters.Status != null)
                employeeQuery = employeeQuery.Where(x => queryparameters.Status.Contains(x.Status));

            if (queryparameters.Type != null)
                employeeQuery = employeeQuery.Where(x => queryparameters.Type.Contains(x.Type));

            var employees = Paginate<EmployeeDTO>(employeeQuery, queryparameters);

            return Ok(employees);
        }

        // GET
        [HttpGet("{id}")]
        public ActionResult GetEmployee(int id)
        {
            var employee = _context.Employee.Where(x => x.Id == id)
                                            .ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider)
                                            .FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> PostEmployee(Employee employee)
        {
            var employeePost = _context.Employee.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(employeePost);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        //Patch
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Employee> value)
        {
            try
            {
                var result = _context.Employee.FirstOrDefault(n => n.Id == id);

                if (result == null)
                    return NotFound();

                value.ApplyTo(result, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

                _context.SaveChanges();

                if (false == ModelState.IsValid)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}