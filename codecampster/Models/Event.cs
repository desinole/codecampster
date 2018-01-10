using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SocialMediaHashtag { get; set; }
        public System.DateTime EventStart { get; set; }
        public System.DateTime EventEnd { get; set; }
        public string CompleteAddress { get; set; }
        public bool IsCurrent { get; set; }
        public bool? AttendeeRegistrationOpen { get; set; }
        public bool? SpeakerRegistrationOpen { get; set; }
    }
}
