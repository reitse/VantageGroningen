using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions")]
    public class HeatStart
    {
        public HeatStart(PresentationSource presentationSource, DateTime when)
        {
            PresentationSource = presentationSource;
            When = when;
        }

        [DataMember]
        public PresentationSource PresentationSource { get; private set; }

        [DataMember]
        public DateTime When { get; private set; }

        public DateTime WhenLocal => When.ToLocalTime();
    }
}