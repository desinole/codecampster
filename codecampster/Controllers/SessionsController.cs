using System.Linq;
using codecampster.Models;
using codecampster.Services;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using codecampster.ViewModels.Session;
using System.Collections.Generic;

namespace codecampster.Controllers
{
    public class SessionsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private ApplicationDbContext _context;

        public SessionsController(
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


        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Client)]
        public IActionResult Agenda()
        {
            ViewBag.Timeslots = _context.Timeslots.OrderBy(t => t.Rank);
            ViewBag.TrackCount = _context.Tracks.Count();
            ViewBag.Tracks = _context.Tracks;
            IQueryable<Session> sessions = _context.Sessions.Include(s => s.Speaker).Include(s => s.Track).Include(s => s.Timeslot).OrderBy(x => Guid.NewGuid());
            return View(sessions.ToList());
        }


        [ResponseCache(Duration = 300,Location=ResponseCacheLocation.Client)]
        public IActionResult Index(string track, string timeslot)
        {
            ViewBag.Timeslots = _context.Timeslots.Where(s=> (!(s.Special == true))).OrderBy(t => t.Rank);
            ViewBag.Tracks = _context.Tracks.OrderBy(x => x.Name);
            IQueryable<Session> sessions = _context.Sessions.Where(s => (!(s.Special == true))).Include(s => s.Speaker).Include(s => s.Track).Include(s => s.Timeslot).OrderBy(x => Guid.NewGuid());
            ViewData["Title"] = string.Format("All {0} Sessions",sessions.Count());
            if (!string.IsNullOrEmpty(track))
            {
                int trackID = 0;
                if (int.TryParse(track, out trackID))
                {
                    var tr = _context.Tracks.Single(t => t.ID == trackID);
                    ViewData["Title"] = string.Format("{0} Track Sessions {1}", tr.Name, tr.RoomNumber);
                    return View(sessions.Where(s => (s.TrackID == trackID) && (!(s.Special == true))).ToList());
                }
            }
            if (!string.IsNullOrEmpty(timeslot))
            {
                int timeslotId = 0;
                if (int.TryParse(timeslot, out timeslotId))
                {
                    var ts = _context.Timeslots.Single(t => t.ID == timeslotId);
                    ViewData["Title"] = string.Format("{0} - {1} Sessions", ts.StartTime, ts.EndTime);
                    return View(sessions.Where(s => s.TimeslotID == timeslotId).ToList());
                }
            }
            return View(sessions.ToList());
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session session = _context.Sessions.Include(s => s.Speaker).Include(s => s.Track).Include(s => s.Timeslot).Single(m => m.SessionID == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            ViewData["SpeakerID"] = new SelectList(_context.Speakers, "ID", "Speaker");
            return View();
        }

        // POST: Sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Sessions.Add(session);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["SpeakerID"] = new SelectList(_context.Speakers, "ID", "Speaker", session.SpeakerID);
            return View(session);
        }

        // GET: Sessions/Edit/5
        public IActionResult Edit(int? id)
        {
            SessionViewModel model = new SessionViewModel();
            ViewData["Level"] = new SelectList(GetLevels(), "Key", "Value");
            if (id.HasValue)
            {
                var sessionDetails = _context.Sessions.Include(s=>s.Speaker).Include(s=>s.Speaker.AppUser).Where(s=>s.SessionID == id.Value).FirstOrDefault();
                if (sessionDetails != null)// && sessionDetails.Speaker.AppUser.UserName==
                {

                    model = new SessionViewModel()
                    {
                        Title = sessionDetails.Name,
                        Description = sessionDetails.Description,
                        CoSpeakers = sessionDetails.CoSpeakers,
                        Keywords = sessionDetails.KeyWords,
                        Level = sessionDetails.Level
                    };
                    ViewData["Level"] = new SelectList(GetLevels(), "Key", "Value", sessionDetails.Level);
                }
                else
                {
                    return NotFound();
                }
            }

            return View(model);
        }

        // POST: Sessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SessionViewModel model, int? id)
        {

            if (ModelState.IsValid)
            {
                var speaker = _context.Speakers.Where(s => s.AppUser.Email == User.Identity.Name).FirstOrDefault();
                if (id.HasValue)
                {
                    Session session = _context.Sessions.Find(id.Value);
                    session.Name = model.Title;
                    session.CoSpeakers = model.CoSpeakers;
                    session.Description = model.Description;
                    session.KeyWords = model.Keywords;
                    session.Level = model.Level;
                    _context.Update(session);
                }
                else
                {
                    Session session = new Session();
                    session.Name = model.Title;
                    session.CoSpeakers = model.CoSpeakers;
                    session.Description = model.Description;
                    session.KeyWords = model.Keywords;
                    session.Level = model.Level;
                    session.SpeakerID = speaker.ID;
                    _context.Sessions.Add(session);
                }
                _context.SaveChanges();
                return RedirectToAction("Sessions", "Speakers");
            }
            Dictionary<int, string> levels = new Dictionary<int, string>();
            levels.Add(1, "All skill levels");
            levels.Add(2, "Some prior knowledge needed");
            levels.Add(3, "Deep Dive");
            ViewData["Level"] = new SelectList(GetLevels(), "Key", "Value", model.Level);
            return View(model);
        }

        private Dictionary<int, string> GetLevels()
        {
            Dictionary<int, string> levels = new Dictionary<int, string>();
            levels.Add(1, "All skill levels");
            levels.Add(2, "Some prior knowledge needed");
            levels.Add(3, "Deep Dive");
            return levels;
        }

        // GET: Sessions/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session session = _context.Sessions.Single(m => m.SessionID == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Session session = _context.Sessions.Single(m => m.SessionID == id);
            _context.Sessions.Remove(session);
            _context.SaveChanges();
            return RedirectToAction("Sessions", "Speakers");
        }
    }
}
