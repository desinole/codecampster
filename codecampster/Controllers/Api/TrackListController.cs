using Codecamp2018.Models;
using codecampster.Models.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace codecampster.Controllers.Api
{
    [Route("api/[controller]")]
    public class TrackListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrackListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrackList
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public IEnumerable<TrackSummary> GetTracks()
        {
            var tracks = _context.Tracks
                .Select(t => new TrackSummary
                {
                    Id = t.ID,
                    Name = t.Name,
                    RoomNumber = t.RoomNumber
                })
                .OrderBy(t => t.Name)
                .ToList();

            return tracks;
        }
    }
}