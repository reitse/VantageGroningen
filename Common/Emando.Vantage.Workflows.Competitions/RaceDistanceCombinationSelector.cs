using System;
using System.Diagnostics;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class RaceDistanceCombinationSelector : IRaceSelector
    {
        private readonly DistanceCombination distanceCombination;

        public RaceDistanceCombinationSelector(DistanceCombination distanceCombination)
        {
            if (distanceCombination.Distances == null)
                throw new ArgumentNullException("distanceCombination.Distances");

            this.distanceCombination = distanceCombination;
        }

        public override string ToString()
        {
            return distanceCombination.Number.ToString();
        }

        #region IRaceSelector Members

        public IQueryable<Race> Query(IQueryable<Race> times)
        {
            var distanceIdentifiers = distanceCombination.Distances.Select(d => d.Id).ToArray();
            return from r in times
                   where distanceIdentifiers.Contains(r.Distance.Id)
                    && r.Competitor.DistanceCombinations.Any(dc => dc.DistanceCombinationId == distanceCombination.Id && dc.Status == DistanceCombinationCompetitorStatus.Confirmed)
                   select r;
        }

        #endregion
    }
}