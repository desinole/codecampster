using System.Collections.Generic;
using System.Linq;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Codecamp2018
{
    [Produces("application/json")]
    [Route("api/SponsorList")]
    public class SponsorListController : Controller
    {
        private ApplicationDbContext _context;
        public SponsorListController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/TimeslotList
        [HttpGet]
        //[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IEnumerable<Sponsor> GetSponsors()
        {
            return _context.Sponsors.OrderBy(s => s.CompanyName);
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
