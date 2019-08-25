using System.Collections.Generic;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class HistoricalTimeSelectorBindingModel
    {
        public string Key { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}