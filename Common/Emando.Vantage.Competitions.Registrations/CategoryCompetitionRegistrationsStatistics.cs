namespace Emando.Vantage.Competitions.Registrations
{
    public class CategoryCompetitionRegistrationsStatistics
    {
        public CategoryCompetitionRegistrationsStatistics(string discipline, string category, string currency, decimal? amount, int confirmedCount, int withdrawnCount)
        {
            Discipline = discipline;
            Category = category;
            Currency = currency;
            Amount = amount;
            ConfirmedCount = confirmedCount;
            WithdrawnCount = withdrawnCount;
        }

        public string Discipline { get; private set; }

        public string Category { get; private set; }

        public string Currency { get; private set; }

        public decimal? Amount { get; private set; }

        public int ConfirmedCount { get; private set; }

        public int WithdrawnCount { get; private set; }
    }
}