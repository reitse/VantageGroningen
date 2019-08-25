using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class RaceResult : IRaceResult
    {
        [Key, Column(Order = 0), DataMember]
        public Guid RaceId { get; set; }

        public virtual Race Race { get; set; }

        [Key, Column(Order = 1), Required, StringLength(50), DataMember]
        public string InstanceName { get; set; }

        [DataMember]
        public RaceStatus Status { get; set; }

        #region IRaceResult Members

        [DataMember]
        public decimal? Points { get; set; }

        [DataMember]
        public TimeInvalidReason? TimeInvalidReason { get; set; }

        #endregion
    }
}