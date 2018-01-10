using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.Models
{
    public class AttendeeSession
    {
        public int ID { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser AppUser { get; set; }
        public int SessionID { get; set; }
        [ForeignKey("SessionID")]
        public Session RelatedSession { get; set; }
    }
}
