using System.Collections.Generic;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class Pair
    {
        public Pair(int number, Race inner, int innerColor, IEnumerable<CalculatedLap> innerLaps, Race outer, int outerColor, IEnumerable<CalculatedLap> outerLaps)
        {
            Number = number;
            Inner = inner;
            InnerColor = innerColor;
            InnerLaps = innerLaps;
            Outer = outer;
            OuterColor = outerColor;
            OuterLaps = outerLaps;
        }

        public int Number { get; }

        public Race Inner { get; }

        public int InnerColor { get; set; }

        public IEnumerable<CalculatedLap> InnerLaps { get; set; }

        public Race Outer { get; }

        public int OuterColor { get; set; }

        public IEnumerable<CalculatedLap> OuterLaps { get; set; }
    }
}