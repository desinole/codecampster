using System.Diagnostics;
using Codecamp2018.Models;

namespace codecampster.Models.Api
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EventDetail:Event
    {
        public string ContactEmail { get; set; }
        // TODO Move to Event
        public string GPSLatitude { get; set; }
        public string GPSLongitude { get; set; }

        public EventDetail(Event currentEvent) : this()
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

        public EventDetail()
        {
            this.ContactEmail = "board@onetug.org";
        }

        private string DebuggerDisplay => $"{Name}";
    }
}
