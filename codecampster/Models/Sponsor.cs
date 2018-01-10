using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.Models
{
    public class Sponsor
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string SponsorLevel { get; set; }
        public string Bio { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }
        public string AvatarURL { get; set; }
    }
}
