using System.Collections.Generic;

namespace codecampster.Models
{
    public class Speaker
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }
        public string Blog { get; set; }
        public string AvatarURL { get; set; }
        //to display or hide speakers, for instance, the organizers
        public bool? Special { get; set; }

        public List<Session> Sessions { get; set; }
    }
}