using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysqltest.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mysqltest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubEventsController : ControllerBase
    {
        private readonly ClubsContext _context;

        public ClubEventsController(ClubsContext context)
        {
            _context = context;
        }

        // GET: api/ClubEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubEvent>>> GetClubEvent()
        {
            return await _context.ClubEvent.ToListAsync();
        }

        // GET: api/ClubEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubEvent>> GetClubEvent(int id)
        {
            var clubEvent = await _context.ClubEvent.FindAsync(id);

            if (clubEvent == null)
            {
                return NotFound();
            }

            return clubEvent;
        }

        // PUT: api/ClubEvents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClubEvent(int id, ClubEvent clubEvent)
        {
            if (id != clubEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(clubEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ClubEvents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ClubEvent>> PostClubEvent(ClubEvent clubEvent)
        {
            _context.ClubEvent.Add(clubEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClubEvent", new { id = clubEvent.Id }, clubEvent);
        }

        // DELETE: api/ClubEvents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClubEvent>> DeleteClubEvent(int id)
        {
            var clubEvent = await _context.ClubEvent.FindAsync(id);
            if (clubEvent == null)
            {
                return NotFound();
            }

            _context.ClubEvent.Remove(clubEvent);
            await _context.SaveChangesAsync();

            return clubEvent;
        }

        private bool ClubEventExists(int id)
        {
            return _context.ClubEvent.Any(e => e.Id == id);
        }
    }
}