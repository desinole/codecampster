using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codecampster.Models
{
    public class Speaker
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }
        public string Blog { get; set; }
        public string AvatarURL { get; set; }
        public string MVPDetails { get; set; }
        public string AuthorDetails { get; set; }
        public string NoteToOrganizers { get; set; }
        public bool IsMvp { get; set; }
        public string PhoneNumber { get; set; }
        public string LinkedIn { get; set; }
        //to display or hide speakers, for instance, the organizers
        public bool? Special { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser AppUser { get; set; }

        public List<Session> Sessions { get; set; }
    }
}