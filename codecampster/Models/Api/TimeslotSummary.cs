using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace codecampster.Models.Api
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TimeslotSummary
    {
        public int Id { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "h:mm tt")]
        public DateTime StartTime { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "h:mm tt")]
        public DateTime EndTime { get; set; }

        public int Rank { get; set; }

        //to display or hide timeslots, for instance, keynote, lunch, etc
        public bool Special { get; set; }

        private string DebuggerDisplay => $"{Rank}: {StartTime:t} {EndTime:t}";
    }

    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
