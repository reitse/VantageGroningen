namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class CompetitionStatisticsViewModel
    {
        public CategoryCompetitionRegistrationsStatisticsViewModel[] Registrations { get; set; }

        public CategoryLicensePurchasesStatisticsViewModel[] LicensePurchases { get; set; }

        public int Transactions { get; set; }
    }
}