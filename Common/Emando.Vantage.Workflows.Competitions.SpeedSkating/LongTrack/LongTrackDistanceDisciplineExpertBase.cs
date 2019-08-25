using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack
{
    public abstract class LongTrackDistanceDisciplineExpertBase : DistanceDisciplineExpertBase
    {
        public override IDisciplineCalculator DisciplineCalculator { get; } = new LongTrackDisciplineCalculator();
    }
}