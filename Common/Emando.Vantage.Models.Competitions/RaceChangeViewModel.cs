using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceChangeViewModel
    {
        public Guid Id { get; set; }

        public Guid CompetitorId { get; set; }

        public int Heat { get; set; }

        public int Lane { get; set; }

        public int Color { get; set; }
    }
}