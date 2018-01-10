using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Codecamp2018.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Codecamp2018.Controllers
{
    [Authorize]
    public class TracksController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private ApplicationDbContext _context;
        // GET: /<controller>/
        public TracksController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ILoggerFactory loggerFactory,
           ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<SpeakersController>();
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Tracks.ToList());
        }

        // GET: Sessions/Edit/5
        public IActionResult Edit(int? id)
        {
            Track model = new Track();
            if (id.HasValue)
            {
                var track = _context.Tracks.Where(s => s.ID == id.Value).FirstOrDefault();
                if (track != null)// && sessionDetails.Speaker.AppUser.UserName==
                {

                    model = new Track()
                    {
                        Name = track.Name,
                        RoomNumber = track.RoomNumber
                    };
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
        public IActionResult Edit(Track model, int? id)
        {

            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    Track track  = _context.Tracks.Find(id.Value);
                    track.Name = model.Name;
                    track.RoomNumber = model.RoomNumber;
                    _context.Update(track);
                }
                else
                {
                    Track track = new Track();
                    track.Name = model.Name;
                    track.RoomNumber = model.RoomNumber;
                    _context.Tracks.Add(track);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Tracks");
            }
            return View(model);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var track = _context.Tracks.Single(m => m.ID == id);
            _context.Tracks.Remove(track);
            _context.SaveChanges();
            return RedirectToAction("Index", "Tracks");
        }
    }
}
