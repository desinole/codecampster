using System.ComponentModel.DataAnnotations;

namespace codecampster.ViewModels.Session
{
    public class SessionViewModel
    {
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
    }
}
