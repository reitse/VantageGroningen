namespace Emando.Vantage.Competitions.Registrations
{
    public class CompetitionSerieRegistrationsStatistics
    {
        public CompetitionSerieRegistrationsStatistics(string discipline, string currency, decimal? amount, int count)
        {
            Discipline = discipline;
            Amount = amount;
            Currency = currency;
            Count = count;
        }

        public string Discipline { get; private set; }

        public decimal? Amount { get; private set; }

        public string Currency { get; private set; }

        public int Count { get; private set; }
    }
}