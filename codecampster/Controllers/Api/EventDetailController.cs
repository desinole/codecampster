using Codecamp2018.Models;
using codecampster.Models.Api;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace codecampster.Controllers.Api
{
    [Route("api/[controller]")]
    // Old Name
    [Route("api/EventDetails")]
    public class EventDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventDetails
        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult GetEvent()
        {
            var currentEvent = _context.Events
                .Where(e=> e.IsCurrent)
                .FirstOrDefault();

            if (currentEvent == null)
                return NotFound();

            var eventDetails = new EventDetail(currentEvent);

            return new ObjectResult(eventDetails);
        }
    }
}
