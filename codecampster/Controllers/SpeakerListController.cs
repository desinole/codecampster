using System.Collections.Generic;
using System.Linq;
using codecampster.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace codecampster.Controllers
{
    [Produces("application/json")]
    [Route("api/SpeakerList")]
    public class SpeakerListController : Controller
    {
        private ApplicationDbContext _context;
        public SpeakerListController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/TimeslotList
        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IEnumerable<Speaker> GetSpeakers()
        {
            return _context.Speakers.OrderBy(s=>s.FullName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
