using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Server.Services.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions")]
    public class DistanceTimingWorkflowServiceSettings
    {
        [DataMember]
        public TimeSpan StartRecurrenceThreshold { get; set; }

        [DataMember]
        public TimeSpan LapSwapDelta { get; set; }

        [DataMember]
        public RaceTimingWorkflowServiceSettings RaceTimingWorkflowSettings { get; set; }

        [DataMember]
        public TimeSpan TimePrecision { get; set; }

        [DataMember]
        public TimeSpan RaceTransponderSeenThrottle { get; set; }

        [DataMember]
        public bool EnablePrediction { get; set; }
    }
}