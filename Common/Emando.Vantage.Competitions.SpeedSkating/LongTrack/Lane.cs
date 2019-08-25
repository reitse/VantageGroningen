using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions.SpeedSkating.LongTrack
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions/SpeedSkating/LongTrack")]
    public enum Lane
    {
        [EnumMember]
        Inner,
        [EnumMember]
        Outer
    }

    public static class LaneExtensions
    {
        public static Lane Opposite(this Lane lane)
        {
            return lane == Lane.Inner ? Lane.Outer : Lane.Inner;
        }

        public static string ToShortString(this Lane lane)
        {
            switch (lane)
            {
                case Lane.Inner:
                    return "I";
                case Lane.Outer:
                    return "O";
                default:
                    throw new ArgumentOutOfRangeException(nameof(lane));
            }
        }
    }
}