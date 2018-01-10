using Codecamp2018.Models;

namespace Codecamp2018.ViewModels.Event
{
    public class EventViewModel:Codecamp2018.Models.Event
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

        public Codecamp2018.Models.Event ToBase()
        {
            var theEvent = new Codecamp2018.Models.Event();
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

        public void FromBase(Codecamp2018.Models.Event theEvent)
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
