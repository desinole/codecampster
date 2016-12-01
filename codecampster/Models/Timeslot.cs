using System.Collections.Generic;

namespace codecampster.Models
{
    public class Timeslot
    {
        public int ID { get; set; }
        //refactor to use proper time data types
        public string StartTime { get; set; } 
        public string EndTime { get; set; }
        public int Rank { get; set; }
        //to display or hide timeslots, for instance, keynote, lunch, etc
        public bool? Special { get; set; }

        public List<Session> Sessions { get; set; }
    }
}
