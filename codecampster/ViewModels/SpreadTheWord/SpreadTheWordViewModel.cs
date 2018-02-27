using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;

// ReSharper disable UseNameofExpression

namespace codecampster.ViewModels.SpreadTheWord
{
    public class SpreadTheWordViewModel
    {
        // Sessions

        public IList<SessionSummary> SessionSummaries { get; set; }

        public IList<SelectListItem> SessionSelectList { get; set; }

        public int SessionCount { get; set; }

        // Speakers

        public IList<SpeakerSummary> SpeakerSummaries { get; set; }

        public IList<SelectListItem> SpeakerSelectList { get; set; }

        public int SpeakerCount { get; set; }

        // Sponsors

        public IList<SponsorSummary> SponsorSummaries { get; set; }

        public IList<SelectListItem> SponsorSelectList { get; set; }

        // Tracks

        public IList<TrackSummary> TrackSummaries { get; set; }

        public IList<SelectListItem> TrackSelectList { get; set; }

        public int TrackCount { get; set; }
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SessionSummary
    {
        public int SessionId { get; set; }

        public string SessionName { get; set; }

        public string SpeakerName { get; set; }

        //public string CoSpeakers { get; set; }

        //public Track Track { get; set; }

        //public Timeslot Timeslot { get; set; }

        public string CodeCampUrl { get; set; }

        private string DebuggerDisplay => $"{SpeakerName} - {SessionName}";
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SpeakerSummary
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Twitter { get; set; }

        public string Website { get; set; }

        public string Blog { get; set; }

        public string LinkedIn { get; set; }

        public string CodeCampUrl { get; set; }

        private string DebuggerDisplay => $"{FullName}";
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SponsorSummary
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string SponsorLevel { get; set; }

        public string Twitter { get; set; }

        public string Website { get; set; }

        public string CodeCampUrl { get; set; }

        private string DebuggerDisplay => $"{CompanyName} - {SponsorLevel}";
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TrackSummary
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string RoomNumber { get; set; }

        public string CodeCampUrl { get; set; }

        private string DebuggerDisplay => $"{Name}";
    }
}
