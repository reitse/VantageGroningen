using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class Distance : IDistance
    {
        public Distance()
        {
            FirstHeat = 1;
            Rounds = 1;
            StartWeather = new Weather();
            EndWeather = new Weather();
        }

        [Key, DataMember]
        public Guid Id { get; set; }

        [Index("UK_Distances_CompetitionId_Number", Order = 0, IsUnique = true)]
        [DataMember]
        public Guid CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual ICollection<DistanceCombination> Combinations { get; set; }

        public Guid? DistancePointsTableId { get; set; }

        [DataMember]
        public virtual DistancePointsTable DistancePointsTable { get; set; }

        [DataMember]
        public TimeSpan ClassificationPrecision { get; set; }

        [DataMember]
        public DateTime? Starts { get; set; }

        [DataMember]
        public DateTime? LastRaceCommitted { get; set; }

        [DataMember]
        public bool ContinuousNumbering { get; set; }

        [DataMember]
        public virtual ICollection<Race> Races { get; set; }

        [DataMember]
        [StringLength(200)]
        public string Starter { get; set; }

        [DataMember]
        [StringLength(200)]
        public string Referee1 { get; set; }

        [DataMember]
        [StringLength(200)]
        public string Referee2 { get; set; }

        [DataMember]
        public Weather StartWeather { get; set; }

        [DataMember]
        public Weather EndWeather { get; set; }

        [DataMember]
        [StringLength(50)]
        public string VenueCode { get; set; }

        [DataMember]
        [StringLength(100)]
        public string VenueDiscipline { get; set; }

        public virtual Venue Venue { get; set; }

        #region IDistance Members

        [Required, DataMember]
        [StringLength(100)]
        public string Discipline { get; set; }

        [Range(0, int.MaxValue)]
        [Index("UK_Distances_CompetitionId_Number", Order = 1, IsUnique = true)]
        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public decimal TrackLength { get; set; }

        [DataMember]
        [Range(0, int.MaxValue)]
        public int Value { get; set; }

        [DataMember]
        [Range(1, int.MaxValue)]
        public int Rounds { get; set; }

        [DataMember]
        public DistanceValueQuantity ValueQuantity { get; set; }

        [Required, StringLength(100), DataMember]
        public string Name { get; set; }

        [DataMember]
        public DistanceStartMode StartMode { get; set; }

        [DataMember]
        [Range(1, int.MaxValue)]
        public int FirstHeat { get; set; }

        #endregion

        public override string ToString()
        {
            return $"{Number}. {Name}";
        }
    }
}