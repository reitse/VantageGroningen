using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions.SpeedSkating.LongTrack
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions/SpeedSkating/LongTrack")]
    public enum PairsRaceColor
    {
        [EnumMember]
        White,
        [EnumMember]
        Red,
        [EnumMember]
        Yellow,
        [EnumMember]
        Blue
    }

    public static class PairsRaceColorExtensions
    {
        public static string ToShortString(this PairsRaceColor color)
        {
            switch (color)
            {
                case PairsRaceColor.White:
                    return "Wh";
                case PairsRaceColor.Red:
                    return "Rd";
                case PairsRaceColor.Yellow:
                    return "Yw";
                case PairsRaceColor.Blue:
                    return "Bl";
                default:
                    throw new ArgumentOutOfRangeException(nameof(color));
            }
        }
    }
}