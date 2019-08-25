using System;
using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class PointsRankedRace : RankedRace
    {
        public PointsRankedRace(int? ranking, Race race, IReadOnlyDictionary<int, decimal> lapPoints, decimal totalPoints, int lapCount,
            bool sameRankingAsPrevious = false)
            : base(ranking, race, totalPoints, sameRankingAsPrevious)
        {
            LapPoints = lapPoints ?? throw new ArgumentNullException(nameof(lapPoints));
            LapCount = lapCount;
        }

        public IReadOnlyDictionary<int, decimal> LapPoints { get; }

        public int LapCount { get; }
    }
}