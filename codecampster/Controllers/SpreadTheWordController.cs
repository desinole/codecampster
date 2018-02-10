using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using codecampster.ViewModels.SpreadTheWord;
using Codecamp2018.Models;
using Codecamp2018.ViewModels.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace codecampster.Controllers
{
    public class SpreadTheWordController : Controller
    {
        private ApplicationDbContext _context;

        public SpreadTheWordController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            // Sessions

            var approvedSessionSummaries = _context.Sessions
                .Where(s => s.IsApproved)
                .Where(s => s.Special != true)
                .Include(s => s.Speaker)
                .Include(s => s.Speaker.AppUser)
                //.Include(s => s.Track)
                //.Include(s => s.Timeslot)
                .Select(s => new SessionSummary
                {
                    SessionId = s.SessionID,
                    SessionName = s.Name,
                    SpeakerName =
                        string.Format("{0} {1}",
                            s.Speaker.AppUser.FirstName,
                            s.Speaker.AppUser.LastName),
                    OnetugUrl = $"{baseUrl}/Sessions/Details/{s.SessionID}"
                })
                .OrderBy(s => s.SpeakerName)
                .ThenBy(s => s.SessionName)
                .ToList();

            var approvedSessionSelectList = approvedSessionSummaries
                .Select((s,i) => new SelectListItem
                {
                    Text = $"{s.SpeakerName} - {s.SessionName}",
                    Value = i.ToString()
                })
                .ToList();

            var approvedSessionCount = approvedSessionSelectList.Count();

            // Speakers

            var approvedSpeakerSummaries = _context.Speakers
                .Where(s => s.Sessions.Any(c => c.IsApproved))
                .Where(s => s.Special != true)
                .Include(s => s.Sessions)
                .Include(s => s.AppUser)
                // Don't need to show all Speaker data for social media
                .Select(s => new SpeakerSummary
                {
                    Id = s.ID,
                    // TODO
                    FullName = $"{s.AppUser.FirstName} {s.AppUser.LastName}",
                    Twitter = s.Twitter,
                    Website = s.Website,
                    Blog = s.Blog,
                    LinkedIn = s.LinkedIn,
                    OnetugUrl = $"{baseUrl}/Speakers/Details/{s.ID}"
                })
                .OrderBy(s => s.FullName)
                .ToList();

            var approvedSpeakerSelectList = approvedSpeakerSummaries
                .Select((s, i) => new SelectListItem
                {
                    Text = s.FullName,
                    Value = i.ToString()
                })
                .ToList();

            var approvedSpeakerCount = approvedSpeakerSelectList.Count();

            // Sponsors

            var sponsorSummaries = _context.Sponsors
                // Don't need to show all Sponsor data for social media
                .Select(s => new SponsorSummary
                {
                    Id = s.ID,
                    CompanyName = s.CompanyName,
                    SponsorLevel = s.SponsorLevel,
                    Twitter = s.Twitter,
                    Website = s.Website,
                    OnetugUrl = $"{baseUrl}/Sponsors/Details/{s.ID}"
                })
                .ToList();

            var sponsorSelectList = sponsorSummaries
                .Select((s, i) => new SelectListItem
                {
                    Text = $"{s.CompanyName} - {s.SponsorLevel}",
                    Value = i.ToString()
                })
                .ToList();

            // ViewModel

            var viewModel = new SpreadTheWordViewModel
            {
                // Sessions
                SessionSelectList = approvedSessionSelectList,
                SessionSummaries = approvedSessionSummaries,
                SessionCount = approvedSessionCount,

                // Speakers
                SpeakerSelectList = approvedSpeakerSelectList,
                SpeakerSummaries = approvedSpeakerSummaries,
                SpeakerCount = approvedSpeakerCount,

                // Sponsors
                SponsorSelectList = sponsorSelectList,
                SponsorSummaries = sponsorSummaries,
            };

            return View(viewModel);
        }
    }
}
