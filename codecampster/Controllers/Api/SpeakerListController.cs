using codecampster.Models.Api;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace codecampster.Controllers.Api
{
    [Route("api/[controller]")]
    public class SpeakerListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpeakerListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SpeakerList
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public IEnumerable<SpeakerSummary> GetSpeakers()
        {
            var approvedSpeakers = _context.Speakers
                .Where(speaker => speaker.Sessions.Any(session => session.IsApproved))
                .Select(s => new SpeakerSummary
                {
                    Id = s.ID,
                    FullName = s.FullName,
                    Company = s.Company,
                    Title = s.Title,
                    Bio = s.Bio,
                    Twitter = s.Twitter,
                    Website = s.Website,
                    Blog = s.Blog,
                    AvatarUrl = s.AvatarURL,
                    MvpDetails = s.MVPDetails,
                    AuthorDetails = s.AuthorDetails,
                    IsMvp = s.IsMvp,
                    LinkedIn = s.LinkedIn,
                    Special = s.Special
                })
                .OrderBy(s => s.FullName)
                .ToList();

            return approvedSpeakers;
        }
    }
}
