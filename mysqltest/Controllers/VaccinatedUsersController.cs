using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using mysqltest.Mapping.DTO;
using mysqltest.Models;
using mysqltest.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mysqltest.Controllers
{
    public class VaccinatedUsersController : ApiControllerBase
    {
        public VaccinatedUsersController(ClubsContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaccinatedUser>>> GetVaccinatedUsers([FromQuery] QueryVaccinatedParameters queryparameters)
        {
            var vaccin_userQuery = _context.VaccinatedUsers
                                     .OrderBy(c => c.Id) //ordering all students by Id
                                     .AsQueryable(); //To apply filters

            //Applying filters:

            if (queryparameters.Status != null)
                vaccin_userQuery = vaccin_userQuery.Where(s => queryparameters.Status.Contains(s.VaccinatedStatus));

            if (queryparameters.Type != null)
                vaccin_userQuery = vaccin_userQuery.Where(t => queryparameters.Type.Contains(t.vaccinatedType));

            var students = Paginate<VaccinatedDTO>(vaccin_userQuery, queryparameters); //using Paginate with Parameters we have alredy set

            return Ok(students);
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<VaccinatedUser>> GetVaccinatedUser(long id)
        {
            var vaccin_user = _context.Students
                               .Where(x => x.Id == id) //searching for Student using Id
                               .ProjectTo<VaccinatedDTO>(_mapper.ConfigurationProvider) //using mapper and StudentDTO
                               .FirstOrDefault(); //Selecting the club by Id with StudentDTO parameters or make it as a default

            if (vaccin_user == null)
                return NotFound();

            return Ok(vaccin_user);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> PutVaccinatedUser(VaccinatedUser vaccinated)
        {
            var post_vaccinated = _context.VaccinatedUsers.Add(vaccinated);

            await _context.SaveChangesAsync();

            return CreatedAtRoute("Post", new { Id = vaccinated.Id }, vaccinated);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<VaccinatedUser>> PostVaccinatedUser(VaccinatedUser vaccinatedUser)
        {
            _context.VaccinatedUsers.Add(vaccinatedUser);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaccinatedUser", new { id = vaccinatedUser.Id }, vaccinatedUser);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<VaccinatedUser>> DeleteVaccinatedUser(long id)
        {
            var vaccinatedUser = await _context.VaccinatedUsers.FindAsync(id);

            if (vaccinatedUser == null)
                return NotFound();

            _context.VaccinatedUsers.Remove(vaccinatedUser);

            await _context.SaveChangesAsync();

            return vaccinatedUser;
        }
    }
}