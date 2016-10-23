using System;
using System.Linq;
using codecampster.Models;
using codecampster.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace codecampster.Controllers
{
    public class HomeController : Controller
    {
        private IOptions<AppSettings> _appSettings;
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
            ApplicationDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _context = context;
            _appSettings = appSettings;
            _logger = loggerFactory.CreateLogger<HomeController>();
        }
        public IActionResult Index()
        {
            ViewBag.Announcements = _context.Announcements.Where(a => a.PublishOn < DateTime.Now && a.ExpiresOn > DateTime.Now).OrderBy(a => a.Rank);
            ViewBag.Speakers = _context.Speakers.Select(s => ((s.Special == null ? false : s.Special.Value))).ToList().Where(s => !s).Count();
            ViewBag.Attendees = _context.ApplicationUsers.Select(a => (a.RSVP == null ? false : a.RSVP.Value)).ToList().Where(a => a).Count();
            ViewBag.Sessions = _context.Sessions.Select(s => ((s.Special == null ? false : s.Special.Value))).ToList().Where(s => !s).Count();
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
