using System.Linq;
using Codecamp2018.Models;
using codecampster.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace codecampster.Controllers.Api
{
    [Route("api/[controller]")]
    public class SpeakerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SpeakerController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public SpeakerSummary Get(string speakerName)
        {
            return _db.Speakers
                .Where(speaker => speaker.FullName.ToLower() == speakerName.ToLower())
                .Select(speaker => new SpeakerSummary
                {
                    Id = speaker.ID,
                    FullName = speaker.FullName,
                    Company = speaker.Company,
                    Title = speaker.Title,
                    Bio = speaker.Bio,
                    Twitter = speaker.Twitter,
                    Website = speaker.Website,
                    Blog = speaker.Blog,
                    AvatarUrl = speaker.AvatarURL,
                    MvpDetails = speaker.MVPDetails,
                    AuthorDetails = speaker.AuthorDetails,
                    IsMvp = speaker.IsMvp,
                    LinkedIn = speaker.LinkedIn,
                    Special = speaker.Special,
                    Sessions = speaker.Sessions
                        .Where(session => session.IsApproved)
                        .Select(session => new SessionSummary
                        {
                            Id = session.SessionID,
                            Name = session.Name,
                            Description = session.Description,
                            Level = session.Level,
                            KeyWords = session.KeyWords,
                            Special = session.Special,
                            SpeakerId = session.SpeakerID,
                            CoSpeakerId = session.CoSpeakerID,
                            CoSpeakers = session.CoSpeakers,
                            TrackId = session.TrackID,
                            TimeslotId = session.TimeslotID
                        }).ToArray()
                })
                .FirstOrDefault();
        }
    }
}
