using System;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Competitions.Test
{
    public class MockRacePassing : IReadOnlyRacePassing
    {
        public MockRacePassing(long @where, int when)
        {
            Where = @where;
            When = new DateTime(2015, 2, 5, 17, 16, 0) + TimeSpan.FromSeconds(when);
        }

        public Guid RaceId { get; private set; }

        public string InstanceName { get; private set; }

        public string How { get; private set; }

        public long Where { get; }

        public DateTime When { get; }

        public TimeSpan Time { get; private set; }

        public decimal? Passed { get; private set; }

        public decimal? Speed { get; private set; }

        public RaceEventFlags Flags { get; private set; }

        public PresentationSource PresentationSource { get; private set; }
    }
}