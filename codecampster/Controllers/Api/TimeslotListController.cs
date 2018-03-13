using Codecamp2018.Models;
using codecampster.Models.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace codecampster.Controllers.Api
{
    [Route("api/[controller]")]
    public class TimeslotListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeslotListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TimeslotList
        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public IEnumerable<TimeslotSummary> GetTimeslots()
        {
            var timeslots = _context.Timeslots
                .Select(t => new TimeslotSummary
                {
                    Id = t.ID,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Rank = t.Rank,
                    Special = t.Special
                })
                .OrderBy(t => t.Rank)
                .ToList();

            return timeslots;
        }
    }
}
