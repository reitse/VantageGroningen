using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class RacePassingViewModel : PassingViewModel, IReadOnlyRacePassing, IHaveRacePassingKey
    {
        public Guid RaceId { get; set; }

        public string InstanceName { get; set; }

        public PresentationSource PresentationSource { get; set; }

        public DateTime When { get; set; }

        public RaceEventFlags Flags { get; set; }
    }
}