namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class StatisticsViewModel
    {
        public CompetitionSerieRegistrationsStatisticsViewModel[] SerieRegistrations { get; set; }

        public RegistrationsStatisticsViewModel[] Registrations { get; set; }

        public LicensePurchasesStatisticsViewModel[] LicensePurchases { get; set; }

        public int Transactions { get; set; }
    }
}