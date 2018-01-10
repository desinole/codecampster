using System.ComponentModel.DataAnnotations;
using Codecamp2018.ViewModels.Speaker;

namespace Codecamp2018.ViewModels.Session
{
    public class SessionViewModel
    {
        // Session ID, dot displayed
        public int SessionID { get; set; }
        // Speaker ID, not displayed
        public int SpeakerID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Target Audience")]
        public int Level { get; set; }
        [Display(Name = "Keywords (comma separated)")]
        public string Keywords { get; set; }
        [Display(Name = "List Co-Speakers (if any)")]
        public string CoSpeakers { get; set; }
        [Display(Name = "Approved Session")]
        public bool IsApproved { get; set; }
        public string TrackName { get; set; }
        public string RoomNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        // Referring URL, used to navigate back to invoking
        // page
        public string ReferringUrl { get; set; }
        public SpeakerViewModel Speaker { get; set; }
    }
}
