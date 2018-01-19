using System;
using System.Linq;
using Codecamp2018.Models;
using Codecamp2018.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Codecamp2018.ViewModels.Speaker;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Codecamp2018.ViewModels.Session;
using System.Collections.Generic;

namespace Codecamp2018.Controllers
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
            var speaker = (from _speaker in _context.Speakers
                           join applicationUser in _context.ApplicationUsers on _speaker.ApplicationUserId equals applicationUser.Id
                           where applicationUser.Email == User.Identity.Name
                           select new SpeakerViewModel
                           {
                               ID = _speaker.ID,
                               AvatarURL = _speaker.AvatarURL,
                               Bio = _speaker.Bio,
                               Blog = _speaker.Blog,
                               Company = _speaker.Company,
                               Title = _speaker.Title,
                               Twitter = _speaker.Twitter,
                               Website = _speaker.Website,
                               MVPDetails = _speaker.MVPDetails,
                               AuthorDetails = _speaker.AuthorDetails,
                               NoteToOrganizers = _speaker.NoteToOrganizers,
                               IsMvp = _speaker.IsMvp,
                               PhoneNumber = _speaker.PhoneNumber,
                               LinkedIn = _speaker.LinkedIn,
                               FullName = (applicationUser.FirstName.Length > 0 ? applicationUser.FirstName : string.Empty)
                                          + " " + (applicationUser.LastName.Length > 0 ? applicationUser.LastName : string.Empty)
                           }).FirstOrDefault();

            var sessions = from session in _context.Sessions
                           where session.SpeakerID == speaker.ID
                           select new SessionViewModel
                           {
                               SessionID = session.SessionID,
                               Title = session.Name,
                               Description = session.Description,
                               Level = session.Level,
                               Keywords = session.KeyWords,
                               CoSpeakers = session.CoSpeakers,
                               IsApproved = session.IsApproved
                           };

            speaker.Sessions = sessions;

            return View(speaker);
        }

        [HttpGet]
        public IActionResult Sessions()
        {
            var speaker = (from _speaker in _context.Speakers
                           join applicationUser in _context.ApplicationUsers on _speaker.ApplicationUserId equals applicationUser.Id
                           where applicationUser.Email == User.Identity.Name
                           select new SpeakerViewModel
                           {
                               ID = _speaker.ID,
                               AvatarURL = _speaker.AvatarURL,
                               Bio = _speaker.Bio,
                               Blog = _speaker.Blog,
                               Company = _speaker.Company,
                               Title = _speaker.Title,
                               Twitter = _speaker.Twitter,
                               Website = _speaker.Website,
                               MVPDetails = _speaker.MVPDetails,
                               AuthorDetails = _speaker.AuthorDetails,
                               NoteToOrganizers = _speaker.NoteToOrganizers,
                               IsMvp = _speaker.IsMvp,
                               PhoneNumber = _speaker.PhoneNumber,
                               LinkedIn = _speaker.LinkedIn,
                               FullName = (applicationUser.FirstName.Length > 0 ? applicationUser.FirstName : string.Empty)
                                          + " " + (applicationUser.LastName.Length > 0 ? applicationUser.LastName : string.Empty)
                           }).FirstOrDefault();

            var sessions = from session in _context.Sessions
                           where session.SpeakerID == speaker.ID
                           select new SessionViewModel
                           {
                               SessionID = session.SessionID,
                               Title = session.Name,
                               Description = session.Description,
                               Level = session.Level,
                               Keywords = session.KeyWords,
                               CoSpeakers = session.CoSpeakers,
                               IsApproved = session.IsApproved
                           };

            speaker.Sessions = sessions;

            return View(speaker);
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
                speaker.IsMvp = model.IsMvp;
                speaker.PhoneNumber = model.PhoneNumber;
                speaker.LinkedIn = model.LinkedIn;
                speaker.FullName = model.FullName;

               _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            if (_context.Events.SingleOrDefault().AttendeeRegistrationOpen ?? false)
            {
                var speakers = _context.Speakers
                    .Include(s => s.Sessions)
                    .Include(s=>s.AppUser)
                    .Where(s => !(s.Special == true)
                    && s.Sessions.Any(c => c.IsApproved)
                    )
                    .OrderBy(x => Guid.NewGuid());
                return View(speakers);
            }
            else
            {
                var speakers = _context.Speakers
                    .Include(s => s.Sessions)
                     .Include(s => s.AppUser)
                   .Where(s => !(s.Special == true))
                    .OrderBy(x => Guid.NewGuid());
                return View(speakers);
            }

        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult Details(int id)
        {
            ViewBag.IsSpeakerSubmissionOpen 
                = _context.Events.SingleOrDefault().SpeakerRegistrationOpen
                ??false;

            // Get the session specified by the supplied session ID
            var sessionVm = from session in _context.Sessions
                            where session.SpeakerID == id
                            select new SessionViewModel
                            {
                                SessionID = session.SessionID,
                                SpeakerID = session.SpeakerID,
                                Title = session.Name,
                                Description = session.Description,
                                Level = session.Level,
                                Keywords = session.KeyWords,
                                CoSpeakers = session.CoSpeakers
                            };

            // Get the speaker for the supplied session ID
            var speakerVm = from speaker in _context.Speakers
                            where speaker.ID == id
                            select new SpeakerViewModel
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
                                NoteToOrganizers = speaker.NoteToOrganizers,
                                IsMvp = speaker.IsMvp,
                                PhoneNumber = speaker.PhoneNumber,
                                LinkedIn = speaker.LinkedIn,
                                FullName = string.IsNullOrEmpty(speaker.FullName)? (speaker.AppUser.FirstName + " " + speaker.AppUser.LastName):speaker.FullName,
                                Sessions = sessionVm != null ? sessionVm : null
                            };

            return View(speakerVm.FirstOrDefault());
        }
    }
}