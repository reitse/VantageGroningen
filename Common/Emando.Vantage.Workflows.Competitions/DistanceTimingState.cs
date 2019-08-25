using System;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class DistanceTimingState : DistanceTimingState<Competition, Distance, RaceState, RacePassingState, RaceLapState>
    {
        public DistanceTimingState(IDistanceDisciplineCalculatorManager calculatorManager) : base(calculatorManager)
        {
        }
    }
}