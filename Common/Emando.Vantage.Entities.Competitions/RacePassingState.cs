using System;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2015/07/Entities/Competitions")]
    public class RacePassingState : IReadOnlyRacePassing, IHaveRacePassingKey
    {
        public RacePassingState(Race race, string instanceName, PresentationSource presentationSource, long @where, DateTime @when, TimeSpan time, decimal? passed,
            decimal? speed, RaceEventFlags flags)
        {
            if (race == null)
                throw new ArgumentNullException(nameof(race));

            Race = race;
            RaceId = race.Id;
            InstanceName = instanceName;
            PresentationSource = presentationSource;
            Where = @where;
            When = when;
            Time = time;
            Passed = passed;
            Speed = speed;
            Flags = flags;
        }

        public Race Race { get; private set; }

        #region IReadOnlyRacePassing Members

        [DataMember]
        public Guid RaceId { get; private set; }

        [DataMember]
        public string InstanceName { get; private set; }

        [DataMember]
        public PresentationSource PresentationSource { get; private set; }

        [DataMember]
        public long Where { get; private set; }

        [DataMember]
        public DateTime When { get; private set; }

        [DataMember]
        public TimeSpan Time { get; private set; }

        [DataMember]
        public decimal? Passed { get; private set; }

        [DataMember]
        public decimal? Speed { get; private set; }

        [DataMember]
        public RaceEventFlags Flags { get; private set; }

        #endregion

        public static RacePassingState FromPassing(RacePassing passing)
        {
            return new RacePassingState(passing.Race, passing.InstanceName, passing.PresentationSource, passing.Where, passing.When, passing.Time, passing.Passed,
                passing.Speed, passing.Flags);
        }
    }
}