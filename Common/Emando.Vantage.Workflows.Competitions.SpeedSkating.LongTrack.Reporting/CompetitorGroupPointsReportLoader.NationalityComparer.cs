using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public partial class CompetitorGroupPointsReportLoader
    {
        private class NationalityComparer : ICompetitorGroupComparer
        {
            public static readonly NationalityComparer Default = new NationalityComparer();

            public bool Equals(CompetitorBase x, CompetitorBase y)
            {
                return x.NationalityCode == y.NationalityCode;
            }

            public int GetHashCode(CompetitorBase obj)
            {
                return obj?.NationalityCode?.GetHashCode() ?? 0;
            }

            public CompetitorGroup Group(CompetitorBase competitor, IEnumerable<RankedRace> races)
            {
                return new CompetitorGroup(competitor.NationalityCode, races);
            }
        }
    }
}
