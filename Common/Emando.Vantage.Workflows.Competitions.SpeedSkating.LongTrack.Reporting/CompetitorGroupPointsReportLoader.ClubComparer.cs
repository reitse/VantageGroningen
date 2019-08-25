using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public partial class CompetitorGroupPointsReportLoader
    {
        private class ClubComparer : ICompetitorGroupComparer
        {
            public static readonly ClubComparer Default = new ClubComparer();

            public bool Equals(CompetitorBase x, CompetitorBase y)
            {
                return x.ClubCountryCode == y.ClubCountryCode && x.ClubCode == y.ClubCode;
            }

            public int GetHashCode(CompetitorBase obj)
            {
                return obj?.ClubCode?.GetHashCode() ?? 0;
            }

            public CompetitorGroup Group(CompetitorBase competitor, IEnumerable<RankedRace> races)
            {
                return new CompetitorGroup(competitor.ClubFullName, races);
            }
        }
    }
}
