using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceTimeViewModel
    {
        public string ApplianceName { get; set; }

        public string ApplianceInstanceName { get; set; }

        public string InstanceName { get; set; }

        public string How { get; set; }

        public TimeSpan Time { get; set; }

        public TimeInfo TimeInfo { get; set; }
    }
}