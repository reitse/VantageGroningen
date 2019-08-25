using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public enum RaceStatus
    {
        [EnumMember]
        Drawn,
        [EnumMember]
        Activated,
        [EnumMember]
        Deactivated,
        [EnumMember]
        Running,
        [EnumMember]
        Done
    }
}