using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions")]
    public enum TimeInvalidReason
    {
        [EnumMember]
        Unknown,
        [EnumMember]
        NotStarted,
        [EnumMember]
        NotFinished,
        [EnumMember]
        Disqualified,
        [EnumMember]
        Withdrawn
    }
}