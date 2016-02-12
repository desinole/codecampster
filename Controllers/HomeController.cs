using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using codecampster.Models;
using codecampster.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace codecampster.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Announcements = _context.Announcements.Where(a => a.PublishOn < DateTime.Now && a.ExpiresOn > DateTime.Now).OrderBy(a => a.Rank);
            ViewBag.Speakers = _context.Speakers.Count();
            var attendees = _context.ApplicationUsers.Select(a=>(a.RSVP==null?false:a.RSVP.Value)).ToList();
            ViewBag.Attendees = attendees.Where(a=>a).Count();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _context.ApplicationUsers.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
                ViewBag.RSVP = currentUser.RSVP;
                ViewBag.Volunteer = currentUser.Volunteer;
                DebugMessage(string.Format("{0} {1} {2}",
                User.Identity.Name, 
                currentUser.RSVP??false,
                currentUser.Volunteer??false));
            }
            ViewBag.Sponsor = _context.Sponsors.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            return View();
        }

        private async void DebugMessage(string message)
        {
            _logger.LogCritical(message);
        }

        public IActionResult About()
        {
            return View();
        }
                        
        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
