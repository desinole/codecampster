using System.Diagnostics;

namespace codecampster.Models.Api
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SpeakerSummary
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public string Bio { get; set; }

        public string Twitter { get; set; }

        public string Website { get; set; }

        public string Blog { get; set; }

        public string AvatarUrl { get; set; }

        public string MvpDetails { get; set; }

        public string AuthorDetails { get; set; }

        public bool IsMvp { get; set; }

        public string LinkedIn { get; set; }
        
        //to display or hide speakers, for instance, the organizers
        public bool? Special { get; set; }

        private string DebuggerDisplay =>
            $"{Id} - {FullName}";
    }
}
