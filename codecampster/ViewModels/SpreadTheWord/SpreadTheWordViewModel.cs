using System.Collections.Generic;
using Codecamp2018.Models;
using Codecamp2018.ViewModels.Session;
using Codecamp2018.ViewModels.Speaker;
using Codecamp2018.ViewModels.Sponsor;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace codecampster.ViewModels.SpreadTheWord
{
    public class SpreadTheWordViewModel
    {
        // Sessions

        public IList<SelectListItem> SessionSelectList { get; set; }

        public IList<SessionSummary> SessionSummaries { get; set; }

        // Speakers

        public IList<SelectListItem> SpeakerSelectList { get; set; }

        public IList<SpeakerSummary> SpeakerSummaries { get; set; }

        // Sponsors

        public IList<SelectListItem> SponsorSelectList { get; set; }

        public IList<SponsorSummary> SponsorSummaries { get; set; }
    }

    public class SessionSummary
    {
        public int SessionId { get; set; }

        public string SessionName { get; set; }

        public string SpeakerName { get; set; }

        //public string CoSpeakers { get; set; }

        //public Track Track { get; set; }

        //public Timeslot Timeslot { get; set; }

        public string OnetugUrl { get; set; }
    }

    public class SpeakerSummary
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Twitter { get; set; }

        public string Website { get; set; }

        public string Blog { get; set; }

        public string LinkedIn { get; set; }

        public string OnetugUrl { get; set; }
    }

    public class SponsorSummary
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string SponsorLevel { get; set; }

        public string Twitter { get; set; }

        public string Website { get; set; }

        public string OnetugUrl { get; set; }
    }
}
