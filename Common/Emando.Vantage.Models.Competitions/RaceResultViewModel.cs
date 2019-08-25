using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceResultViewModel
    {
        public string InstanceName { get; set; }

        public RaceStatus Status { get; set; }

        public TimeInvalidReason? TimeInvalidReason { get; set; }

        public decimal? Points { get; set; }
    }
}