namespace Emando.Vantage.Competitions.Registrations
{
    public class LicensePurchasesStatistics
    {
        public LicensePurchasesStatistics(string discipline, int count, string currency, decimal? amount)
        {
            Discipline = discipline;
            Count = count;
            Amount = amount;
            Currency = currency;
        }

        public string Discipline { get; private set; }

        public int Count { get; private set; }

        public decimal? Amount { get; private set; }

        public string Currency { get; private set; }
    }
}