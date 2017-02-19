using codecampster.Models;

namespace codecampster.ViewModels.Event
{
    public class EventViewModel:codecampster.Models.Event
    {
        public bool IsAttendeeRegistrationOpen
        {
            get
            {
                return base.AttendeeRegistrationOpen.HasValue ? base.AttendeeRegistrationOpen.Value : false;
            }
            set
            {
                this.AttendeeRegistrationOpen = value;
            }
        }

        public bool IsSpeakerRegistrationOpen
        {
            get
            {
                return base.SpeakerRegistrationOpen.HasValue ? base.SpeakerRegistrationOpen.Value : false;
            }
            set
            {
                this.SpeakerRegistrationOpen = value;
            }
        }

        public codecampster.Models.Event ToBase()
        {
            var theEvent = new codecampster.Models.Event();
            theEvent.ID = this.ID;
            theEvent.AttendeeRegistrationOpen = this.IsAttendeeRegistrationOpen;
            theEvent.CompleteAddress = this.CompleteAddress;
            theEvent.EventEnd = this.EventEnd;
            theEvent.EventStart = this.EventStart;
            theEvent.IsCurrent = this.IsCurrent;
            theEvent.Name = this.Name;
            theEvent.SocialMediaHashtag = this.SocialMediaHashtag;
            theEvent.SpeakerRegistrationOpen = this.IsSpeakerRegistrationOpen;
            return theEvent;
        }

        public void FromBase(codecampster.Models.Event theEvent)
        {
            this.ID = theEvent.ID;
            this.CompleteAddress = theEvent.CompleteAddress;
            this.EventEnd = theEvent.EventEnd;
            this.EventStart = theEvent.EventStart;
            this.IsAttendeeRegistrationOpen = theEvent.AttendeeRegistrationOpen.HasValue ? theEvent.AttendeeRegistrationOpen.Value : false;
            this.IsCurrent = theEvent.IsCurrent;
            this.IsSpeakerRegistrationOpen = theEvent.SpeakerRegistrationOpen.HasValue ? theEvent.SpeakerRegistrationOpen.Value : false;
            this.SocialMediaHashtag = theEvent.SocialMediaHashtag;
            this.Name = theEvent.Name;
        }
    }
}
