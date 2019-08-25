using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [Flags]
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions")]
    public enum RaceEventFlags
    {
        [EnumMember]
        None = 0x0,
        [EnumMember]
        Measured = 0x1,
        [EnumMember]
        Estimated = 0x2,
        [EnumMember]
        Moved = 0x4,
        [EnumMember]
        Edited = 0x8,
        [EnumMember]
        Deleted = 0x10,
        [EnumMember]
        Present = 0x20,
        [EnumMember]
        Inserted = 0x40
    }
}