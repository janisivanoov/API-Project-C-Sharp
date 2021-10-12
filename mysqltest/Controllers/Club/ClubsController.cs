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
    public class ClubsController : ApiControllerBase
    {
        public ClubsController(ClubsContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        /// <summary>
        /// GetAll Clubs With Filters
        /// </summary>
        [HttpGet]
        public ActionResult GetClubs([FromQuery] QueryClubParameters queryParameters)
        {
            var clubsQuery = _context.Clubs
                                     .OrderBy(c => c.Id) //ordering all clubs by Id
                                     .AsQueryable(); //To apply filters
            //Applying filters:
            if (queryParameters.Name != null)
                clubsQuery = clubsQuery.Where(n => n.Name.Contains(queryParameters.Name)); //Checking for the club Name in context if entered

            if (queryParameters.Type != null)
                clubsQuery = clubsQuery.Where(x => queryParameters.Type.Contains(x.Type)); //Checking for the club Type in context if entered

            if (queryParameters.Status != null)
                clubsQuery = clubsQuery.Where(s => queryParameters.Status.Contains(s.Status)); //Chacking for the club Status in context if entered

            var clubs = Paginate<ClubTmpDTO>(clubsQuery, queryParameters); //Using Paginate with Parameters we have already set

            return Ok(clubs);
        }

        /// <summary>
        /// Get Club By Id
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult GetClub(int id)
        {
            var club = _context.Clubs
                               .Where(x => x.Id == id) //searching for Club using Id
                               .ProjectTo<ClubDTO>(_mapper.ConfigurationProvider) //using mapper and ClubDTO
                               .FirstOrDefault(); //Selecting the club by Id with ClubDTO parameters or make it as a default

            if (club == null)
                return NotFound();

            return Ok(club);
        }

        /// <summary>
        /// Post Club
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostClubAsync(Club clubPost)
        {
            var post_club = _context.Clubs
                                    .Add(clubPost); //using Add function to post a club

            await _context.SaveChangesAsync(); //saving edit in a database

            return Ok(clubPost);
        }

        /// <summary>
        /// Delete Club
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Club>> DeleteClub(int id)
        {
            var club = await _context.Clubs
                                     .FindAsync(id); //searching for club using Id
            if (club == null)
                return NotFound();

            _context.Clubs.Remove(club); //using Remove function

            await _context.SaveChangesAsync(); //saving all in database

            return club;
        }

        /// <summary>
        /// Patch the Club
        /// </summary>
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Club> value)
        {
            try
            {
                var result = _context.Clubs.FirstOrDefault(n => n.Id == id); //Getting Club by Id

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