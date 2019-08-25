using System;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class Passing : IReadOnlyPassing
    {
        [DataMember]
        public TimeSpan Time { get; set; }
        
        [DataMember]
        public decimal? Passed { get; set; }
        
        [DataMember]
        public decimal? Speed { get; set; }

        [DataMember]
        public long Where { get; set; }
    }
}