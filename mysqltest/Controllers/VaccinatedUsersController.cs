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
    public class VaccinatedUsersController : ApiControllerBase
    {
        public VaccinatedUsersController(ClubsContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        // GET
        [HttpGet]
        public ActionResult GetVaccinatedUsers([FromQuery] QueryVaccinatedParameters queryparameters)
        {
            var vaccin_userQuery = _context.VaccinatedUsers
                                     .OrderBy(c => c.Id) //ordering all users by Id
                                     .AsQueryable(); //To apply filters

            //Applying filters:

            if (queryparameters.Status != null)
                vaccin_userQuery = vaccin_userQuery.Where(s => queryparameters.Status.Contains(s.VaccinatedStatus)); //If status unequals to null return it

            if (queryparameters.Type != null)
                vaccin_userQuery = vaccin_userQuery.Where(t => queryparameters.Type.Contains(t.VaccinatedType)); //If type unequals to null return it

            var vaccin_user = Paginate<VaccinatedDTO>(vaccin_userQuery, queryparameters); //using Paginate with Parameters we have alredy set

            return Ok(vaccin_user);
        }

        // GET
        [HttpGet("{id}")]
        public ActionResult GetVaccinatedUser(long id)
        {
            var vaccin_user = _context.VaccinatedUsers
                               .Where(x => x.Id == id) //searching for user using Id
                               .ProjectTo<VaccinatedDTO>(_mapper.ConfigurationProvider) //using mapper and StudentDTO
                               .FirstOrDefault(); //Selecting the user by Id with VaccinatedDTO parameters or make it as a default

            if (vaccin_user == null)
                return NotFound();

            return Ok(vaccin_user);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> PostVaccinatedUser([FromBody] VaccinatedUser vaccinatedUser)
        {
            _context.VaccinatedUsers.Add(vaccinatedUser); //Using Add method to add vaccinated person

            await _context.SaveChangesAsync(); //Saving added user

            return Ok(vaccinatedUser);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<VaccinatedUser>> DeleteVaccinatedUser(long id)
        {
            var vaccinatedUser = await _context.VaccinatedUsers.FindAsync(id); //Looking for a person

            if (vaccinatedUser == null)
                return NotFound();

            _context.VaccinatedUsers.Remove(vaccinatedUser); //Using remove method to delete it

            await _context.SaveChangesAsync(); //Saving changes

            return vaccinatedUser;
        }

        //Patch
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<VaccinatedUser> value)
        {
            try
            {
                var result = _context.VaccinatedUsers.FirstOrDefault(n => n.Id == id); //Getting user by Id

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