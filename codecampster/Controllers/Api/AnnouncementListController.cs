using Codecamp2018.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace codecampster.Controllers.Api
{
    [Route("api/[controller]")]
    public class AnnouncementListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AnnouncementList
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public IEnumerable<Announcement> GetAnnouncements()
        {
            var announcements = _context.Announcements
                .OrderBy(a=>a.Rank)
                .ToList();

            return announcements;
        }
    }
}
