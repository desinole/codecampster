using System.Diagnostics;
using Codecamp2018.Models;

namespace codecampster.Models.Api
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SponsorDetail : Sponsor
    {
        public int SponsorLevelRank { get; set; }

        private string DebuggerDisplay =>
            $"{SponsorLevelRank} - {SponsorLevel} - {CompanyName}";
    }
}