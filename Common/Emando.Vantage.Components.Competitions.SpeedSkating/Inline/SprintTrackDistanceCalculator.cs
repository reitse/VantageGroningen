using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.Inline
{
    public class SprintTrackDistanceCalculator : DistanceDisciplineCalculator
    {
        public override DistanceRoundScheme RoundScheme(IDistance distance)
        {
            return DistanceRoundScheme.SingleElimination;
        }

        public override int FirstCompetitorsToNextRound(IDistance distance, int round, int heat, int races)
        {
            return 2;
        }

        public override TimeSpan EstimatedRoundDuration(IDistance distance, int round)
        {
            return TimeSpan.FromMinutes(3);
        }
    }
}