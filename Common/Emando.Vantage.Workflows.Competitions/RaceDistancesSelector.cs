using System;
using System.Linq;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class RaceDistancesSelector : IRaceSelector
    {
        private readonly Distance[] distances;

        public RaceDistancesSelector(Distance[] distances)
        {
            this.distances = distances;
        }

        public override string ToString()
        {
            return string.Join("-", distances.OrderBy(d => d.Number).Select(d => d.Value));
        }

        #region IRaceSelector Members

        public IQueryable<Race> Query(IQueryable<Race> times)
        {
            var identifiers = distances.Select(d => d.Id).ToArray();
            return from r in times
                   where identifiers.Contains(r.Distance.Id)
                   select r;
        }

        #endregion
    }
}