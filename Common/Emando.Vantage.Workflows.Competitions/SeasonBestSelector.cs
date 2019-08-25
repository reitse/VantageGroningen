using System;

namespace Emando.Vantage.Workflows.Competitions
{
    public class SeasonBestSelector : SeasonTimeSelectorBase
    {
        public SeasonBestSelector(DateTime from, DateTime to) : base(from, to)
        {
        }
    }
}