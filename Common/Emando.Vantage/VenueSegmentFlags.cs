using System;
using System.Runtime.Serialization;

namespace Emando.Vantage
{
    [Flags]
    [DataContract(Namespace = "http://emandovantage.com/2014/02")]
    public enum VenueSegmentFlags
    {
        [EnumMember]
        None = 0x0,
        [EnumMember]
        LaneSwitch = 0x1
    }
}