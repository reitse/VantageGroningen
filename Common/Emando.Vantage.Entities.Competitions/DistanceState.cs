using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public enum DistanceState
    {
        [EnumMember]
        Created,
        [EnumMember]
        Drawn,
        [EnumMember]
        Activating,
        [EnumMember]
        Activated,
        [EnumMember]
        Deactivating,
        [EnumMember]
        Deactivated,
        [EnumMember]
        Done
    }
}