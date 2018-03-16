using System.Diagnostics;

namespace codecampster.Models.Api
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SessionSummary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public string KeyWords { get; set; }

        // to display or hide sessions, for instance, keynote, lunch, etc
        public bool? Special { get; set; }

        public int SpeakerId { get; set; }

        public int? CoSpeakerId { get; set; }

        public string CoSpeakers { get; set; }

        public int? TrackId { get; set; }

        public int? TimeslotId { get; set; }

        private string DebuggerDisplay => $"#{Id} - Ts {TimeslotId} - Tr {TrackId} - {Name}";
    }
}
