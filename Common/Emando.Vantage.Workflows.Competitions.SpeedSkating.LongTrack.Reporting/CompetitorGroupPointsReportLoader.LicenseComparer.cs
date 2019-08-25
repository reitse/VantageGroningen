using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public partial class CompetitorGroupPointsReportLoader
    {
        private class LicenseKeyComparer : ICompetitorGroupComparer
        {
            public static readonly LicenseKeyComparer Default = new LicenseKeyComparer();

            public bool Equals(CompetitorBase x, CompetitorBase y)
            {
                return x.LicenseKey == y.LicenseKey;
            }

            public int GetHashCode(CompetitorBase obj)
            {
                return obj?.LicenseKey?.GetHashCode() ?? 0;
            }

            public CompetitorGroup Group(CompetitorBase competitor, IEnumerable<RankedRace> races)
            {
                return new CompetitorGroup(competitor.FullName, races);
            }
        }
    }
}
