using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class RaceUserResultBindingModel
    {
        public RaceStatus Status { get; set; }

        public TimeInvalidReason? TimeInvalidReason { get; set; }

        public TimeInfo TimeInfo { get; set; }

        public TimeSpan?[] LapTimes { get; set; }
    }
}