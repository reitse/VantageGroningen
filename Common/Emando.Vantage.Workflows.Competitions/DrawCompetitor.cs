using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class DrawCompetitor
    {
        public DrawCompetitor(CompetitorBase competitor, IPersonLicenseTime time)
        {
            Competitor = competitor;
            Time = time;
        }

        public CompetitorBase Competitor { get; private set; }

        public IPersonLicenseTime Time { get; private set; }
    }
}