using System;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Competitions.Test
{
    public class MockRaceLap : IReadOnlyRaceLap
    {
        public MockRaceLap(int time, bool presented, string how, int? fixedIndex = null, int? fixedRanking = null)
        {
            Time = TimeSpan.FromSeconds(time);
            Flags = presented ? RaceEventFlags.Present : RaceEventFlags.None;
            PresentationSource = new PresentationSource("", "", how);
            FixedIndex = fixedIndex;
            FixedRanking = fixedRanking;
        }

        #region IReadOnlyRaceLap Members

        public Guid RaceId { get; private set; }

        public string InstanceName => null;

        public DateTime When => default(DateTime);

        public TimeSpan Time { get; }

        public RaceEventFlags Flags { get; }

        public int? Index { get; private set; }

        public int? Ranking { get; private set; }

        public int? FixedIndex { get; }

        public int? FixedRanking { get; }

        public PresentationSource PresentationSource { get; }

        public decimal? Points { get; private set; }

        #endregion
    }
}