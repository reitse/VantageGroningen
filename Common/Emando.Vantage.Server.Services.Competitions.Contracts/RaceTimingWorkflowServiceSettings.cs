using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Server.Services.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions")]
    public class RaceTimingWorkflowServiceSettings
    {
        [DataMember]
        public TimeSpan PassingWindow { get; set; }

        [DataMember]
        public TimeSpan PassingRecurrenceThreshold { get; set; }

        [DataMember]
        public TimeSpan? BlockBeforeEstimatedTime { get; set; }

        [DataMember]
        public TimeSpan MinimumFirstPassingTime { get; set; }

        [DataMember]
        public bool PhotofinishOverrides { get; set; }

        [DataMember]
        public TimeSpan? AdvanceNextLapIndexAfterLapDelay { get; set; }
    }
}