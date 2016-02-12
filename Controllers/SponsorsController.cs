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
    public class SponsorsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        public SponsorsController(
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
            _logger = loggerFactory.CreateLogger<SponsorsController>();
            _context = context;
        }
        
        [ResponseCache(Duration = 300)]
        public IActionResult Index()
        {
            return View(_context.Sponsors.Select(s=>s).OrderBy(x => Guid.NewGuid()));
        }
        
        [ResponseCache(Duration = 300)]
        public IActionResult Details(int id)
        {
            return View(_context.Sponsors.Where(s=>s.ID==id).SingleOrDefault());
        }
    }
}