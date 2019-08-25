using System;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class Lap : IReadOnlyLap
    {
        #region IReadOnlyLap Members

        [DataMember]
        public TimeSpan Time { get; set; }

        [DataMember]
        public decimal? Points { get; set; }

        #endregion
    }
}