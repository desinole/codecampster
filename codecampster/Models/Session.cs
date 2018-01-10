using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public string KeyWords { get; set; }
        //to display or hide sessions, for instance, keynote, lunch, etc
        public bool? Special { get; set; }
        public bool IsApproved { get; set; }

        public int SpeakerID { get; set; }
        [ForeignKey("SpeakerID")]
        public Speaker Speaker { get; set; }
        public int? CoSpeakerID { get; set; }
        public string CoSpeakers { get; set; }

        public int? TrackID { get; set; }
        [ForeignKey("TrackID")]
        public Track Track { get; set; }

        public int? TimeslotID { get; set; }
        [ForeignKey("TimeslotID")]
        public Timeslot Timeslot { get; set; }
    }
}
