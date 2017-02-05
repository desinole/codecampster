using System;
using System.Linq;
using codecampster.Models;
using codecampster.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using codecampster.ViewModels.Sponsor;

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

#if !DEBUG
		[ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
#endif
		public IActionResult Index()
        {
            return View(_context.Sponsors.Select(s=>s).OrderBy(x => Guid.NewGuid()));
        }

#if !DEBUG
        [ResponseCache(Duration = 300)]
#endif
		public IActionResult Details(int id)
        {
            return View(_context.Sponsors.Where(s=>s.ID==id).SingleOrDefault());
        }

		// GET: Sponsors/Create
		//[Authorize]
		public IActionResult Create()
		{
			ViewData["SponsorID"] = new SelectList(_context.Sponsors, "ID", "Sponsor");
			return View();
		}

		// POST: Sponsors/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Create(Sponsor sponsor)
		{
			if (ModelState.IsValid)
			{
				_context.Sponsors.Add(sponsor);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewData["SponsorID"] = new SelectList(_context.Sponsors, "ID", "Sponsor", sponsor.ID);
			return View(sponsor);
		}

		// GET: Sponsors/Edit/5
		[Authorize]
		public IActionResult Edit(int? id)
		{
			SponsorViewModel model = null;
			if (id.HasValue)
			{
				var sponsorDetails = _context.Sponsors.Where(s => s.ID == id).FirstOrDefault();
				if (sponsorDetails != null)
				{
					model = new SponsorViewModel()
					{
						ID = sponsorDetails.ID,
						AvatarURL = sponsorDetails.AvatarURL,
						Bio = sponsorDetails.Bio,
						CompanyName = sponsorDetails.CompanyName,
						SponsorLevel = sponsorDetails.SponsorLevel,
						Twitter = sponsorDetails.Twitter,
						Website = sponsorDetails.Website
					};
				}
			}

			if (model != null)
				return View(model);
			else
				return NotFound();
		}

		// POST: Sponsors/Edit/5
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(SponsorViewModel model, int? id)
		{
			if (ModelState.IsValid)
			{
				if (id.HasValue)
				{
					Sponsor sponsor = _context.Sponsors.Find(id.Value);
					sponsor.AvatarURL = model.AvatarURL;
					sponsor.Bio = model.Bio;
					sponsor.CompanyName = model.CompanyName;
					sponsor.SponsorLevel = model.SponsorLevel;
					sponsor.Twitter = model.Twitter;
					sponsor.Website = model.Website;

					_context.Update(sponsor);
				}
				else
				{
					Sponsor sponsor = new Sponsor();
					model.ID = id.Value;
					sponsor.ID = id.Value;
					sponsor.AvatarURL = model.AvatarURL;
					sponsor.Bio = model.Bio;
					sponsor.CompanyName = model.CompanyName;
					sponsor.SponsorLevel = model.SponsorLevel;
					sponsor.Twitter = model.Twitter;
					sponsor.Website = model.Website;

					_context.Sponsors.Add(sponsor);
				}
				_context.SaveChanges();
				return RedirectToAction("Sponsors", "Sponsors");
			}

			return View(model);
		}

		// GET: Sponsors/Delete/5
		[Authorize]
		[ActionName("Delete")]
		public IActionResult Delete(int? id)
		{
			if (!id.HasValue)
				return NotFound();

			Sponsor sponsor = _context.Sponsors.SingleOrDefault(m => m.ID == id.Value);
			if (sponsor == null)
				return NotFound();
			else
			{
				_context.Sponsors.Remove(sponsor);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

	}
}