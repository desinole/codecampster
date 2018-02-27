using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.Models
{
    public class Timeslot
    {
        public int ID { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:h:mm tt}")]
        public DateTime StartTime { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:h:mm tt}")]
        public DateTime EndTime { get; set; }
        public int Rank { get; set; }

        //to display or hide timeslots, for instance, keynote, lunch, etc
        public bool Special { get; set; }

        public List<Session> Sessions { get; set; }
    }
}
