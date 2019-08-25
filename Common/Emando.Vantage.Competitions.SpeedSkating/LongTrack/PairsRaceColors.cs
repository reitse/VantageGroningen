using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions.SpeedSkating.LongTrack
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions/SpeedSkating/LongTrack")]
    public enum PairsRaceColors
    {
        [EnumMember]
        WhiteRed,
        [EnumMember]
        YellowBlue
    }

    public static class PairColorExtensions
    {
        public static string ToShortString(this PairsRaceColors colors)
        {
            switch (colors)
            {
                case PairsRaceColors.WhiteRed:
                    return "WhRd";
                case PairsRaceColors.YellowBlue:
                    return "YwBl";
                default:
                    throw new ArgumentOutOfRangeException(nameof(colors));
            }
        }

        public static PairsRaceColors ToPairColor(this PairsRaceColor raceColor)
        {
            switch (raceColor)
            {
                case PairsRaceColor.White:
                case PairsRaceColor.Red:
                    return PairsRaceColors.WhiteRed;
                case PairsRaceColor.Yellow:
                case PairsRaceColor.Blue:
                    return PairsRaceColors.YellowBlue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(raceColor));
            }
        }

        public static bool HasRaceColor(this PairsRaceColors colors, PairsRaceColor raceColor)
        {
            switch (colors)
            {
                case PairsRaceColors.WhiteRed:
                    return raceColor == PairsRaceColor.White || raceColor == PairsRaceColor.Red;
                case PairsRaceColors.YellowBlue:
                    return raceColor == PairsRaceColor.Yellow || raceColor == PairsRaceColor.Blue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(colors));
            }
        }

        public static IEnumerable<PairsRaceColor> GetRaceColors(this PairsRaceColors colors)
        {
            switch (colors)
            {
                case PairsRaceColors.WhiteRed:
                    return new[] { PairsRaceColor.White, PairsRaceColor.Red };
                case PairsRaceColors.YellowBlue:
                    return new[] { PairsRaceColor.Yellow, PairsRaceColor.Blue };
                default:
                    throw new ArgumentOutOfRangeException(nameof(colors));
            }
        }

        public static PairsRaceColor ToLaneColor(this PairsRaceColors colors, Lane lane)
        {
            switch (colors)
            {
                case PairsRaceColors.WhiteRed:
                    return lane == Lane.Inner ? PairsRaceColor.White : PairsRaceColor.Red;
                case PairsRaceColors.YellowBlue:
                    return lane == Lane.Inner ? PairsRaceColor.Yellow : PairsRaceColor.Blue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(colors));
            }
        }
    }
}