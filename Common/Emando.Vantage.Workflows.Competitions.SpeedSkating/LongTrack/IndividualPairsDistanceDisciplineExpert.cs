using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack
{
    public class IndividualPairsDistanceDisciplineExpert : PairsDistanceDisciplineExpertBase
    {
        public override IDistanceDisciplineCalculator Calculator => IndividualPairsDistanceCalculator.Default;
    }
}