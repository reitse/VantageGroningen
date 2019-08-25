using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack
{
    public class TeamPairsDistanceDisciplineExpert : PairsDistanceDisciplineExpertBase
    {
        public override IDistanceDisciplineCalculator Calculator => TeamPairsDistanceCalculator.Default;
    }
}