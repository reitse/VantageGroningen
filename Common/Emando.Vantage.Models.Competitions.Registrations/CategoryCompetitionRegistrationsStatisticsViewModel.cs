namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class CategoryCompetitionRegistrationsStatisticsViewModel
    {
        public string Discipline { get; set; }

        public string Category { get; set; }

        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public int ConfirmedCount { get; set; }

        public int WithdrawnCount { get; set; }
    }
}