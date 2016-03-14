using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using codecampster.Models;
using codecampster.Services;
using codecampster.ViewModels.Account;
using Microsoft.Extensions.OptionsModel;
//using System.Web.Http;

namespace codecampster.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SessionListController : Controller
    {
        private ApplicationDbContext _context;

        public SessionListController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {    
            IQueryable<Session> sessions = _context.Sessions.Include(s => s.Speaker).Include(s => s.Timeslot).OrderBy(x => Guid.NewGuid());
            return Ok(sessions.ToList());
        }
    
    }
}
