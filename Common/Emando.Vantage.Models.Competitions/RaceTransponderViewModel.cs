using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceTransponderViewModel
    {
        public Guid PersonId { get; set; }

        public string Type { get; set; }

        public long Code { get; set; }

        public string Label { get; set; }

        public int? Set { get; set; }
    }
}