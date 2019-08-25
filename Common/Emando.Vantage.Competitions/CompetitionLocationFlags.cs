using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions")]
    public enum CompetitionLocationFlags
    {
        [EnumMember]
        None = 0x0,
        [EnumMember]
        Track = 0x1,
        [EnumMember]
        Road = 0x2
    }
}