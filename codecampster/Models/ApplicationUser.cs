using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace codecampster.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Location { get; set; }
		public string Twitter { get; set; }
		public int? AvatarID { get; set; }
        public bool? RSVP {get;set;}
        public bool? Volunteer {get;set;}
    }
}
