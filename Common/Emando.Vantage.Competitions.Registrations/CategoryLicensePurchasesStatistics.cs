namespace Emando.Vantage.Competitions.Registrations
{
    public class CategoryLicensePurchasesStatistics
    {
        public CategoryLicensePurchasesStatistics(string discipline, string category, int count, string currency, decimal? amount)
        {
            Discipline = discipline;
            Category = category;
            Count = count;
            Amount = amount;
            Currency = currency;
        }

        public string Discipline { get; private set; }

        public string Category { get; set; }

        public int Count { get; private set; }

        public decimal? Amount { get; private set; }

        public string Currency { get; private set; }
    }
}