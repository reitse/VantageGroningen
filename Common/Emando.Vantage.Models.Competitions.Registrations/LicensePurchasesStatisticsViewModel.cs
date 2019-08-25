namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class LicensePurchasesStatisticsViewModel
    {
        public string Discipline { get; set; }

        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public int Count { get; set; }
    }
}