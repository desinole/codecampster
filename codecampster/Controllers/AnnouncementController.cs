using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Codecamp2018.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AnnouncementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AnnouncementController(
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<AnnouncementController>();
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Announcements.OrderBy(a=>a.Rank));
        }

        public IActionResult Create()
        {
            var announcement = new Announcement();
            announcement.Rank = 1;
            return View(new Announcement());
        }

        [HttpPost]
        public IActionResult Create(Announcement announcement)
        {
            _context.Add(announcement);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var announcement = _context.Announcements.Find(id);
            if (_context.Remove(announcement) != null)
                _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
