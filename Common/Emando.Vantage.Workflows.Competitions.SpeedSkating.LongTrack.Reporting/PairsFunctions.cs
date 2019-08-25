using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Telerik.Reporting.Expressions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public static class PairsFunctions
    {
        [Function(Category = "Vantage", Namespace = "SpeedSkating.LongTrack.Pairs")]
        public static string ToShortColor(int color)
        {
            return Resources.ResourceManager.GetString($"Color_{color}");
        }
    }
}