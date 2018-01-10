using System;
using System.Collections.Generic;
using System.Linq;
using Codecamp2018.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Codecamp2018.Controllers
{
    [Produces("application/json")]
    [Route("api/EventDetails")]
    public class EventDetailsController : Controller
    {
        private ApplicationDbContext _context;

        public EventDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrackList
        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public Event GetEvent()
        {
            return new EventDetails(_context.Events.Where(e=>e.IsCurrent).FirstOrDefault());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

    }

    public class EventDetails:Event
    {
        public string ContactEmail { get; set; }
        public string GPSLatitude { get; set; }
        public string GPSLongitude { get; set; }
        public EventDetails(Event currentEvent) : this()
        {
            this.ID = currentEvent.ID;
            this.IsCurrent = currentEvent.IsCurrent;
            this.Name = currentEvent.Name;
            this.SocialMediaHashtag = currentEvent.SocialMediaHashtag;
            this.SpeakerRegistrationOpen = currentEvent.SpeakerRegistrationOpen;
            this.AttendeeRegistrationOpen = currentEvent.AttendeeRegistrationOpen;
            this.CompleteAddress = currentEvent.CompleteAddress;
            this.EventEnd = currentEvent.EventEnd;
            this.EventStart = currentEvent.EventStart;
            this.GPSLatitude = "28.7422565";
            this.GPSLongitude = "-81.30546979999997";
        }

        public EventDetails()
        {
            this.ContactEmail = "board@onetug.org";
        }
    }
}
