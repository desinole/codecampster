using System.Collections.Generic;
using System.Linq;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codecamp2018.Controllers
{
    [Produces("application/json")]
    [Route("api/TrackList")]
    public class TrackListController : Controller
    {
        private ApplicationDbContext _context;

        public TrackListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrackList
        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IEnumerable<Track> GetTracks()
        {
            return _context.Tracks;
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