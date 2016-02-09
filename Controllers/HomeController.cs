using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using codecampster.Models;
using codecampster.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace codecampster.Controllers
{
    public class HomeController : Controller
    {
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
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Announcements = _context.Announcements.Where(a=>a.PublishOn<DateTime.Now && a.ExpiresOn>DateTime.Now).OrderBy(a=>a.Rank);
            ViewBag.Speakers = _context.Speakers.Count();
            Random r = new Random();
            int toSkip = r.Next(0,_context.Sponsors.Count());
            ViewBag.Sponsor = _context.Sponsors.Skip(toSkip).Take(1).FirstOrDefault();
            return View();
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
