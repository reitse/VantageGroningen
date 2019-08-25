using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack
{
    public class MassStartDistanceDisciplineExpert : LongTrackDistanceDisciplineExpertBase
    {
        public override IDistanceDisciplineCalculator Calculator => MassStartDistanceCalculator.Default;

        public override bool FixedLanes => false;

        public override bool LastLapIsAlwaysFinal => false;

        public override Task<IReadOnlyDictionary<int, IReadOnlyCollection<Race>>> FillCompetitorsInHeatsAsync(Distance distance,
            IReadOnlyCollection<Guid> distanceCombinations, int round, IReadOnlyList<IReadOnlyList<CompetitorBase>> competitorGroups, ICompetitionContext context,
            DistanceDrawSettings settings)
        {
            throw new DrawModeNotSupportedException();
        }
    }
}