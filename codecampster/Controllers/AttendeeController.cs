using Codecamp2018.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;

namespace codecampster.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AttendeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AttendeeController(
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<AttendeeController>();
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ApplicationUsers
                .Where(s => s.Speaker == null)
                .OrderBy(a => a.FirstName).ThenBy(a => a.LastName));
        }

        public IActionResult ExportToCsv()
        {
            var columnHeaders = @"First Name, Last Name, Email Address";

            // Get the attendee list
            var attendees = (from attendee in _context.ApplicationUsers
                               select new
                               {
                                   FirstName = attendee.FirstName,
                                   LastName = attendee.LastName,
                                   EmailAddress = attendee.Email,
                               }).ToList();

            // Build the file content
            var attendeeCsv = new StringBuilder();
            attendeeCsv.AppendLine(columnHeaders);
            attendees.ForEach(attendee =>
            {
                // Create the line for the session
                var line = new StringBuilder();
                line.Append(attendee.FirstName.Replace(',', '|')).Append(",")
                    .Append(attendee.LastName.Replace(',', '|')).Append(",")
                    .Append(attendee.EmailAddress);

                // Append the line to the collection of sessions
                attendeeCsv.AppendLine(line.ToString());
            });

            var buffer = Encoding.ASCII.GetBytes(attendeeCsv.ToString());

            return File(buffer, "text/csv", $"CodeCampAttendees.csv");
        }
    }
}
