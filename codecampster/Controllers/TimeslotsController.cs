using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace codecampster.Controllers
{
    public class TimeslotsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private ApplicationDbContext _context;

        public TimeslotsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<TimeslotsController>();
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Timeslots.OrderBy(t => t.StartTime));
        }

        // GET: Timeslots/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                // Get the existing timeslot
                var timeslot
                    = _context.Timeslots
                    .Where(t => t.ID == id.Value)
                    .FirstOrDefault();

                if (timeslot != null)
                    return View(timeslot);
                else
                    return NotFound();
            }
            else
                return View(new Timeslot());
        }

        // POST: Timeslot/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Timeslot timeslot, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    // Get the existing timeslot
                    var theTimeslot
                        = _context.Timeslots
                        .Where(t => t.ID == id.Value)
                        .FirstOrDefault();

                    // Update it
                    theTimeslot.EndTime = timeslot.EndTime;
                    theTimeslot.Rank = timeslot.Rank;
                    theTimeslot.Special = timeslot.Special;
                    theTimeslot.StartTime = timeslot.StartTime;

                    _context.Update(theTimeslot);
                }
                else
                {
                    _context.Timeslots.Add(timeslot);
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Timeslots");
            }

            return View(timeslot);
        }

        [Authorize]
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var timeslot = _context.Timeslots
                .Where(t => t.ID == id.Value)
                .FirstOrDefault();

            if (timeslot == null)
                return NotFound();

            return View(timeslot);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Timeslot timeslot, int id)
        {
            var _timeslot = _context.Timeslots.Single(m => m.ID == id);
            _context.Timeslots.Remove(_timeslot);
            _context.SaveChanges();
            return RedirectToAction("Index", "Timeslots");
        }
    }
}