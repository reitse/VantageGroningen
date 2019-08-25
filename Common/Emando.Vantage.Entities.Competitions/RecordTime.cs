using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2017/01/Entities/Competitions")]
    public class RecordTime : IRecordTime
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        [DataMember]
        public string LicenseIssuerId { get; set; }

        [Key, Column(Order = 1)]
        [DataMember]
        public RecordType Type { get; set; }

        [Key, Column(Order = 2)]
        [DataMember]
        public Gender Gender { get; set; }

        [Key, Column(Order = 3)]
        [DataMember]
        public int FromAge { get; set; }

        [Key, Column(Order = 4)]
        [DataMember]
        public int ToAge { get; set; }

        [StringLength(200)]
        [DataMember]
        public string Name { get; set; }

        public virtual Venue Venue { get; set; }

        [Key, Column(Order = 5)]
        [DataMember]
        public string VenueCode { get; set; }

        [Key, Column(Order = 6)]
        [DataMember]
        public string Discipline { get; set; }

        [Key, Column(Order = 7)]
        [StringLength(100)]
        public string DistanceDiscipline { get; set; }

        [Key, Column(Order = 8)]
        [DataMember]
        public int Distance { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public TimeSpan Time { get; set; }

        [StringLength(3)]
        [DataMember]
        public string NationalityCode { get; set; }
    }
}