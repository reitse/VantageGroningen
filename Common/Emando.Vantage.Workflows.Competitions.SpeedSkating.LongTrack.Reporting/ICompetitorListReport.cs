using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public interface ICompetitorListReport
    {
        IEnumerable<DistanceCombinationCompetitor> Competitors { get; set; } 
    }

    public interface ICompetitorListReportWithOptionalColumn : ICompetitorListReport
    {
        string OptionalFieldValue { get; set; }
    }
}