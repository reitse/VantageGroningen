using Emando.Vantage.Competitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.Inline
{
    public class OneLapTrackDistanceCalculator : DistanceDisciplineCalculator
    {
        public override DistanceRoundScheme RoundScheme(IDistance distance)
        {
            return DistanceRoundScheme.QualificationsToFinal;
        }

        public override TimeSpan EstimatedRoundDuration(IDistance distance, int round)
        {
            return TimeSpan.FromMinutes(10);
        }
    }
}
