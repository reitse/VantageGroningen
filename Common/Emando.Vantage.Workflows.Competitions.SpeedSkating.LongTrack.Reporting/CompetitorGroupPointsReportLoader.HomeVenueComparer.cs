using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public partial class CompetitorGroupPointsReportLoader
    {
        private class HomeVenueComparer : ICompetitorGroupComparer
        {
            public static readonly HomeVenueComparer Default = new HomeVenueComparer();

            public bool Equals(CompetitorBase x, CompetitorBase y)
            {
                return x.VenueCode == y.VenueCode;
            }

            public int GetHashCode(CompetitorBase obj)
            {
                return obj?.VenueCode?.GetHashCode() ?? 0;
            }

            public CompetitorGroup Group(CompetitorBase competitor, IEnumerable<RankedRace> races)
            {
                return new CompetitorGroup(competitor.VenueCode, races);
            }
        }
    }
}
