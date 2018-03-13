using Codecamp2018.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace codecampster.Models.Api
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TrackSummary
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RoomNumber { get; set; }

        private string DebuggerDisplay => $"{Name} - {RoomNumber}";
    }
}
