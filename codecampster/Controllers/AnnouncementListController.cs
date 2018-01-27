using System;
using System.Collections.Generic;
using System.Linq;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codecamp2018.Controllers
{
    [Produces("application/json")]
    [Route("api/AnnouncementList")]
    public class AnnouncementListController : Controller
    {
        private ApplicationDbContext _context;

        public AnnouncementListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrackList
        [HttpGet]
        //[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IEnumerable<Announcement> GetAnnouncements()
        {
            return _context.Announcements.OrderBy(a=>a.Rank);
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
