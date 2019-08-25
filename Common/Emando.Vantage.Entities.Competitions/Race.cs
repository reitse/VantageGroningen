using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions.Properties;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    [KnownType(typeof(PersonCompetitor))]
    [KnownType(typeof(TeamCompetitor))]
    public class Race : IRace, IHaveTransponders
    {
        public Race()
        {
            Round = 1;
            Heat = 1;
        }

        private sealed class RacePresentedTimeComparer : IComparer<Race>
        {
            public int Compare(Race x, Race y)
            {
                if (x.PresentedResult == null)
                    throw new ArgumentException(Resources.RequirePresentedResult, nameof(x));
                if (y.PresentedResult == null)
                    throw new ArgumentException(Resources.RequirePresentedResult, nameof(y));

                if (x.PresentedTime == null && y.PresentedTime == null)
                    return x.PresentedResult.TimeInvalidReason.GetValueOrDefault().CompareTo(y.PresentedResult.TimeInvalidReason.GetValueOrDefault());
                if (x.PresentedTime == null)
                    return 1;
                if (y.PresentedTime == null)
                    return -1;

                return x.PresentedTime.Time.CompareTo(y.PresentedTime.Time);
            }
        }

        public static IComparer<Race> PresentedTimeComparer { get; } = new RacePresentedTimeComparer();

        [Index("UK_Races_DistanceId_Round_Heat_Lane", Order = 0, IsUnique = true, IsClustered = true)]
        [DataMember]
        public Guid DistanceId { get; set; }

        public virtual Distance Distance { get; set; }

        [DataMember]
        public Guid CompetitorId { get; set; }

        [DataMember]
        public virtual CompetitorBase Competitor { get; set; }

        [StringLength(50), DataMember]
        public string PresentedInstanceName { get; set; }

        [DataMember]
        public virtual ICollection<RaceTransponder> Transponders { get; set; }

        public virtual ICollection<RaceStart> Starts { get; set; }

        [DataMember]
        public virtual ICollection<RacePassing> Passings { get; set; }

        [DataMember]
        public virtual ICollection<RaceLap> Laps { get; set; }

        [DataMember]
        public virtual ICollection<RaceResult> Results { get; set; }

        [DataMember]
        public virtual ICollection<RaceTime> Times { get; set; }

        [DataMember]
        public IReadOnlyList<CalculatedLap> EstimatedLaps { get; set; }

        public RaceTime PresentedTime
        {
            get
            {
                if (Times == null || PresentedResult == null || PresentedResult.TimeInvalidReason.HasValue)
                    return null;

                return Times.SingleOrDefault(t => t.InstanceName == PresentedInstanceName);
            }
        }

        public RaceResult PresentedResult => Results?.SingleOrDefault(r => r.InstanceName == PresentedInstanceName && r.Status == RaceStatus.Done);

        public IEnumerable<RaceLap> PresentedLaps => PresentedResult != null ? Laps?.Where(l => l.InstanceName == PresentedInstanceName).Presented() : null;

        #region IHaveTransponders Members

        IEnumerable<TransponderKey> IHaveTransponders.Transponders
        {
            get { return Transponders.Select(t => t.TransponderKey); }
        }

        #endregion

        #region IRace Members

        [Key, DataMember]
        public Guid Id { get; set; }

        ICompetitor IRace.Competitor => Competitor;

        [Range(1, int.MaxValue)]
        [Index("UK_Races_DistanceId_Round_Heat_Lane", Order = 1, IsUnique = true, IsClustered = true)]
        [DataMember]
        public int Round { get; set; }

        [Range(1, int.MaxValue)]
        [Index("UK_Races_DistanceId_Round_Heat_Lane", Order = 2, IsUnique = true, IsClustered = true)]
        [DataMember]
        public int Heat { get; set; }

        [Range(0, int.MaxValue)]
        [Index("UK_Races_DistanceId_Round_Heat_Lane", Order = 3, IsUnique = true, IsClustered = true)]
        [DataMember]
        public int Lane { get; set; }

        [DataMember]
        public int Color { get; set; }

        [DataMember]
        public TimeSpan? PersonalBest { get; set; }

        [DataMember]
        public TimeSpan? SeasonBest { get; set; }

        #endregion

        public override string ToString()
        {
            return Competitor != null
                ? $"{Heat} {Lane} {Competitor.FullName}"
                : $"{Heat} {Lane}";
        }
    }
}