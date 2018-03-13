using codecampster.Models.Api;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace codecampster.Controllers.Api
{

    [Route("api/[controller]")]
    public class SessionListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionListController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(Duration = 300,Location =ResponseCacheLocation.Any)]
        public IEnumerable<SessionSummary> Get()
        {
            var sessions = _context.Sessions
                .Where(s=>s.IsApproved)
                .Select(s => new SessionSummary
                {
                    Id = s.SessionID,
                    Name = s.Name,
                    Description = s.Description,
                    Level = s.Level,
                    KeyWords = s.KeyWords,
                    Special = s.Special,
                    SpeakerId = s.SpeakerID,
                    CoSpeakerId = s.CoSpeakerID,
                    CoSpeakers = s.CoSpeakers,
                    TrackId = s.TrackID,
                    TimeslotId = s.TimeslotID
                })
                // TODO
                //.OrderBy(s => s.TimeslotId)
                //.ThenBy(s => s.TrackId)
                .OrderBy(s => s.Id)
                .ToList();

            return sessions;
        }
    }
}
