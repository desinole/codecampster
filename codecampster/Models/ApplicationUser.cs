using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecamp2018.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string Twitter { get; set; }
        public int? AvatarID { get; set; }
        public bool? RSVP { get; set; }
        public bool? Volunteer { get; set; }

        public Speaker Speaker { get; set; }
    }
}
