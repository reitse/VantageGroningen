using System;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class CompetitionRegistrationsStatisticsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Starts { get; set; }

        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public int ConfirmedCount { get; set; }

        public int WithdrawnCount { get; set; }
    }
}