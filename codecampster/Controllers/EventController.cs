using System.Linq;
using codecampster.Models;
using codecampster.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace codecampster.Controllers
{
    public class EventController : Controller
    {
        private IOptions<AppSettings> _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        public EventController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            IOptions<AppSettings> appSettings,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _appSettings = appSettings;
            _logger = loggerFactory.CreateLogger<EventController>();
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Key = _appSettings.Value.MapKey;
            var locationModel = _context.Events.Where(e=>e.IsCurrent).FirstOrDefault();
            return View(locationModel);
        }
        
        [Authorize]
        public IActionResult RSVP()
        {
            var currentUser = _context.ApplicationUsers.Where(u => u.Email == User.Identity.Name).SingleOrDefault();
            currentUser.RSVP = (!(currentUser.RSVP??false));
            _context.SaveChanges();
            DebugMessage(string.Format("{0} RSVP {1}",currentUser.UserName, currentUser.RSVP));
            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        public IActionResult Volunteer()
        {
            var currentUser = _context.ApplicationUsers.Where(u => u.Email == User.Identity.Name).SingleOrDefault();
            currentUser.Volunteer = (!(currentUser.Volunteer??false));
            _context.SaveChanges();
            DebugMessage(string.Format("{0} Volunteer {1}",currentUser.UserName, currentUser.Volunteer));
            return RedirectToAction("Index", "Home");
        }
        private async void DebugMessage(string message)
        {
            _logger.LogCritical(message);
        }    
    }
}