using System.Collections.Generic;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public interface IPairsDrawReport
    {
        IEnumerable<Pair> Pairs { get; set; }
    }

    public interface IPairsDrawReportWithOptionalColumn : IPairsDrawReport
    {
        string InnerOptionalFieldValue { get; set; }

        string OuterOptionalFieldValue { get; set; }
    }
}