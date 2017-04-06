using System.ComponentModel.DataAnnotations.Schema;

namespace codecampster.Models
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
