using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using codecampster.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace codecampster.Controllers
{
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
        // GET: /<controller>/
        [Authorize(Roles = "administator")]
        public IActionResult Index()
        {
            return View(_context.Announcements.OrderBy(a=>a.Rank));
        }
    }
}
