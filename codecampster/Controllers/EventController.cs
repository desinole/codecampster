using System.Linq;
using Codecamp2018.Models;
using Codecamp2018.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Codecamp2018.ViewModels.Event;

namespace Codecamp2018.Controllers
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
            EventViewModel model = new EventViewModel();
            model.FromBase(locationModel);
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var model = _context.Events.Find(id);
                EventViewModel eventModel = new EventViewModel();
                eventModel.FromBase(model);
                return View(eventModel);
            }
            else
            {
                EventViewModel eventModel = new EventViewModel();
                var model = _context.Events.Where(e => e.IsCurrent).FirstOrDefault();
                if (model != null)
                    eventModel.FromBase(model);
                return View(eventModel);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(EventViewModel model)
        {
            if (model.ID==null)
            {
                _context.Events.Add(model.ToBase());
            }
            else
            {
                var originalEvent = _context.Events.Find(model.ID);
                originalEvent.IsCurrent = model.IsCurrent;
                originalEvent.EventStart = model.EventStart;
                originalEvent.EventEnd = model.EventEnd;
                originalEvent.AttendeeRegistrationOpen = model.AttendeeRegistrationOpen;
                originalEvent.SpeakerRegistrationOpen = model.SpeakerRegistrationOpen;
                originalEvent.SocialMediaHashtag = model.SocialMediaHashtag;
                originalEvent.Name = model.Name;
                _context.Update(originalEvent);
            }
            return RedirectToAction("Index");
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