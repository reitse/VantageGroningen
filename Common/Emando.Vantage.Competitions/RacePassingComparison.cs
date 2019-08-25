using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions")]
    public class RacePassingComparison
    {
        public RacePassingComparison(Guid raceId, string instanceName, PresentationSource presentationSource, DateTime when, TimeSpan time, TimeSpan gap)
        {
            RaceId = raceId;
            InstanceName = instanceName;
            PresentationSource = presentationSource;
            When = when;
            Time = time;
            Gap = gap;
        }

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
        public TimeSpan Gap { get; private set; }
    }
}