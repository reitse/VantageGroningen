using System;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class ClassifiedRace
    {
        public ClassifiedRace(Race race, int? distanceRanking)
        {
            Race = race;
            DistanceRanking = distanceRanking;
        }

        public Race Race { get; }

        public int? DistanceRanking { get; }
    }
}