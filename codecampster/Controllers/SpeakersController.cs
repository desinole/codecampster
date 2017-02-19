using System;
using System.Linq;
using codecampster.Models;
using codecampster.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using codecampster.ViewModels.Speaker;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace codecampster.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public SpeakersController(
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
            _logger = loggerFactory.CreateLogger<SpeakersController>();
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit()
        {
            var speaker = _context.Speakers
                .Where(s=>s.AppUser.Email == User.Identity.Name)
                .FirstOrDefault();
                
            SpeakerViewModel model = new SpeakerViewModel()
            {
                AvatarURL = speaker.AvatarURL,
                Bio = speaker.Bio,
                Blog = speaker.Blog,
                Company = speaker.Company,
                Title = speaker.Title,
                Twitter = speaker.Twitter,
                Website = speaker.Website,
                MVPDetails = speaker.MVPDetails,
                AuthorDetails = speaker.AuthorDetails,
                NoteToOrganizers = speaker.NoteToOrganizers
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Sessions()
        {
            var speaker = _context.Speakers
                .Include(s => s.Sessions)
                .Where(s => s.AppUser.Email == User.Identity.Name)
                .FirstOrDefault();

            return View(speaker.Sessions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(SpeakerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var speaker = _context.Speakers
                    .Where(s => s.AppUser.Email == User.Identity.Name)
                    .FirstOrDefault();

                speaker.AvatarURL = model.AvatarURL;
                speaker.Title = model.Title;
                speaker.Bio = model.Bio;
                speaker.Blog = model.Blog;
                speaker.Company = model.Company;
                speaker.Twitter = model.Twitter;
                speaker.Website = model.Website;
                speaker.MVPDetails = model.MVPDetails;
                speaker.AuthorDetails = model.AuthorDetails;
                speaker.NoteToOrganizers = model.NoteToOrganizers;

               _context.SaveChanges();
            }

            return View(model);
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            var speakers = _context.Speakers
                .Include(s => s.Sessions)
                .Where(s => !(s.Special == true) 
                    && s.Sessions.Any())
                .OrderBy(x => x.FullName);

            return View(speakers);
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult Details(int id)
        {
            ViewBag.IsSpeakerSubmissionOpen = _context.Events.SingleOrDefault().SpeakerRegistrationOpen??false;
            return View(_context.Speakers.Include(s => s.Sessions).Where(s => s.ID == id).SingleOrDefault());
        }
    }
}