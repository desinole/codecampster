using Codecamp2018.Models;
using codecampster.Models.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace codecampster.Controllers.Api
{
    [Route("api/[controller]")]
    public class SponsorListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponsorListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SponsorList
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public IEnumerable<Sponsor> GetSponsors()
        {
            var sponsors = _context.Sponsors
                .Select(s => new SponsorDetail
                {
                    ID = s.ID,
                    // TODO Clean up and normalize sponsor level text
                    SponsorLevel = s.SponsorLevel,
                    SponsorLevelRank = GetSponsorLevelRank(s),
                    CompanyName = s.CompanyName,
                    Bio = s.Bio,
                    Twitter = s.Twitter,
                    Website = s.Website,
                    AvatarURL = s.AvatarURL
                })
                .OrderBy(s => s.SponsorLevelRank)
                .ThenBy(s => s.CompanyName)
                .ToList();

            return sponsors;
        }

        public int GetSponsorLevelRank(Sponsor sponsor)
        {
            var sponsorLevel = sponsor.SponsorLevel.ToLower();

            if (sponsorLevel.Contains("diamond"))
                return 1;

            if (sponsorLevel.Contains("lunch"))
                return 2;

            if (sponsorLevel.Contains("platinum"))
                return 3;

            if (sponsorLevel.Contains("gold"))
                return 4;

            if (sponsorLevel.Contains("party"))
                return 5;

            if (sponsorLevel.Contains("silver"))
                return 6;

            if (sponsorLevel.Contains("bronze"))
                return 7;

            if (sponsorLevel.Contains("breakfast"))
                return 8;

            if (sponsorLevel.Contains("coffee"))
                return 9;

            return 10;
        }
    }
}
