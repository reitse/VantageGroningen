﻿using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.Inline
{
    public class MarathonTrackDistanceCalculator : DistanceDisciplineCalculator
    {
        public override DistanceRoundScheme RoundScheme(IDistance distance)
        {
            return DistanceRoundScheme.QualificationsToFinal;
        }

        public override TimeSpan EstimatedRoundDuration(IDistance distance, int round)
        {
            return TimeSpan.FromMinutes(15);
        }
    }
}