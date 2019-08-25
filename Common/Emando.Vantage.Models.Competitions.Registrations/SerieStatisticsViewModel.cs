namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class SerieStatisticsViewModel
    {
        public CompetitionSerieRegistrationsStatisticsViewModel[] SerieRegistrations { get; set; }

        public CompetitionRegistrationsStatisticsViewModel[] Competitions { get; set; }

        public LicensePurchasesStatisticsViewModel[] LicensePurchases { get; set; }

        public int Transactions { get; set; }
    }
}