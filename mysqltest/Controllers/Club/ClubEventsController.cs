using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using mysqltest.Mapping;
using mysqltest.Models;
using mysqltest.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace mysqltest.Controllers
{
    public class ClubEventsController : ApiControllerBase
    {
        public ClubEventsController(ClubsContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        // GET
        [HttpGet]
        public ActionResult GetClubEvent([FromQuery] QueryClubEventParameters queryParameters)
        {
            var clubseventQuery = _context.ClubEvent
                                      .OrderBy(c => c.Id)
                                      .AsQueryable();
            //Applying filters:
            if (queryParameters.Name != null)
                clubseventQuery = clubseventQuery.Where(n => n.Name.Contains(queryParameters.Name));

            if (queryParameters.Type != null)
                clubseventQuery = clubseventQuery.Where(x => queryParameters.Type.Contains(x.Type));

            if (queryParameters.EventStatus != null)
                clubseventQuery = clubseventQuery.Where(s => queryParameters.EventStatus.Contains(s.EventStatus));

            var clubEvent = Paginate<ClubEventDTO>(clubseventQuery, queryParameters);

            return Ok(clubEvent);
        }

        // GET
        [HttpGet("{id}")]
        public ActionResult GetClubEvent(int id)
        {
            var clubEvent = _context.ClubEvent.Where(x => x.Id == id).ProjectTo<ClubEventDTO>(_mapper.ConfigurationProvider).FirstOrDefault();

            if (clubEvent == null)
                return NotFound();

            return Ok(clubEvent);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> PostClubEvent(ClubEvent clubEvent)
        {
            var post_event = _context.ClubEvent.Add(clubEvent);

            await _context.SaveChangesAsync();

            return Ok(clubEvent);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClubEvent>> DeleteClubEvent(int id)
        {
            var clubEv = await _context.ClubEvent
                                      .FindAsync(id);
            if (clubEv == null)
                return NotFound();

            _context.ClubEvent.Remove(clubEv);

            await _context.SaveChangesAsync();

            return clubEv;
        }

        //Patch
        [HttpPatch("{id}")]
        public ActionResult Patch(long id, [FromBody] JsonPatchDocument<ClubEvent> value)
        {
            try
            {
                var result = _context.ClubEvent.FirstOrDefault(n => n.Id == id);

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