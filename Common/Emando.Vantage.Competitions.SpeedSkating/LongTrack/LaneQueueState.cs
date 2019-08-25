using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions.SpeedSkating.LongTrack
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions/SpeedSkating/LongTrack")]
    public class LaneQueueState
    {
        [DataMember]
        public Dictionary<Lane, Guid[]> Queues { get; set; }

        [DataMember]
        public Guid[] Bench { get; set; }
    }
}