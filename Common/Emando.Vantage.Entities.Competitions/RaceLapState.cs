using System;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2015/07/Entities/Competitions")]
    public class RaceLapState : IReadOnlyActiveRaceLap, IHaveRacePassingKey
    {
        private RaceLapState(Race race, string instanceName, PresentationSource presentationSource, DateTime @when, TimeSpan time, RaceEventFlags flags, decimal? points,
            decimal? totalPoints, int? index, int? ranking, int? fixedIndex, int? fixedRanking)
        {
            if (race == null)
                throw new ArgumentNullException(nameof(race));

            Race = race;
            RaceId = race.Id;
            InstanceName = instanceName;
            PresentationSource = presentationSource;
            When = when;
            Time = time;
            Flags = flags;
            Points = points;
            TotalPoints = totalPoints;
            Index = index;
            Ranking = ranking;
            FixedIndex = fixedIndex;
            FixedRanking = fixedRanking;
        }

        public Race Race { get; private set; }

        [DataMember]
        public int? Index { get; private set; }

        [DataMember]
        public int? Ranking { get; private set; }

        #region IReadOnlyRaceLap Members

        [DataMember]
        public Guid RaceId { get; private set; }

        [DataMember]
        public string InstanceName { get; private set; }

        [DataMember]
        public PresentationSource PresentationSource { get; private set; }

        [DataMember]
        public DateTime When { get; private set; }

        [DataMember]
        public TimeSpan Time { get; private set; }

        [DataMember]
        public RaceEventFlags Flags { get; private set; }

        [DataMember]
        public decimal? Points { get; private set; }

        public decimal? TotalPoints { get; set; }

        [DataMember]
        public int? FixedIndex { get; private set; }

        [DataMember]
        public int? FixedRanking { get; private set; }

        #endregion

        public static RaceLapState FromLap(RaceLap lap, decimal? totalPoints = null)
        {
            return new RaceLapState(lap.Race, lap.InstanceName, lap.PresentationSource, lap.When, lap.Time, lap.Flags, lap.Points, totalPoints, lap.Index, lap.Ranking,
                lap.FixedIndex, lap.FixedRanking);
        }
    }
}