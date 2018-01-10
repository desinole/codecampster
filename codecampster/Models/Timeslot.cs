using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.Models
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
