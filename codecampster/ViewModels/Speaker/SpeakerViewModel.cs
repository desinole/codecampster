using System.ComponentModel.DataAnnotations;

namespace codecampster.ViewModels.Speaker
{
    public class SpeakerViewModel
    {
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }
        [Display(Name = "Twitter")]
        public string Twitter { get; set; }
        [Display(Name = "Website")]
        public string Website { get; set; }
        [Display(Name = "Blog")]
        public string Blog { get; set; }
        [Display(Name = "Avatar link (ideally 250x250px image)")]
        public string AvatarURL { get; set; }
    }
}
