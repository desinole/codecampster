using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace codecampster.Models
{
    public class Session
    {
        public int SessionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }

        public int SpeakerID { get; set; }
        [ForeignKey("SpeakerID")]
        public Speaker Speaker { get; set; }
    }
}
