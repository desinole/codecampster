using System.ComponentModel.DataAnnotations;

namespace codecampster.ViewModels.Sponsor
{
    public class SponsorViewModel
    {
        [Display(Name = "Sponsor ID")]
        public int ID { get; set; }

		[Display(Name = "Avatar link (ideally 250x250px image)")]
		public string AvatarURL { get; set; }

		[Display(Name = "Biography")]
		public string Bio { get; set; }

		[Display(Name = "Company Name")]
		public string CompanyName { get; set; }

		[Display(Name = "Sponsor Level")]
		public string SponsorLevel { get; set; }

		[Display(Name = "Twitter Handle")]
        public string Twitter { get; set; }

		[Display(Name = "Website")]
        public string Website { get; set; }
    }
}
