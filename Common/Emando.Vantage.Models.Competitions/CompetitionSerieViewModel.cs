using System;

namespace Emando.Vantage.Models.Competitions
{
    public class CompetitionSerieViewModel
    {
        public Guid Id { get; set; }

        public string LicenseIssuerId { get; set; }

        public string Discipline { get; set; }

        public int Season { get; set; }

        public string Name { get; set; }

        public int CompetitionsCount { get; set; }
    }
}