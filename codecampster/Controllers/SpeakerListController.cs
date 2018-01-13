using System.Collections.Generic;
using System.Linq;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Codecamp2018.Controllers
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
            var approvedSpeakers = from speakers in _context.Speakers
                                   join sessions in _context.Sessions
                                   on speakers.ID equals sessions.SpeakerID
                                   where sessions.IsApproved
                                   select speakers;
            return approvedSpeakers.OrderBy(s => s.AppUser.Email).Distinct<Speaker>();
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
