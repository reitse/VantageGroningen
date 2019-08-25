namespace Emando.Vantage.Competitions.Registrations
{
    public class RegistrationsStatistics
    {
        public RegistrationsStatistics(string discipline, string currency, decimal? amount, int confirmedCount, int withdrawnCount)
        {
            Discipline = discipline;
            Amount = amount;
            Currency = currency;
            ConfirmedCount = confirmedCount;
            WithdrawnCount = withdrawnCount;
        }

        public string Discipline { get; private set; }

        public decimal? Amount { get; private set; }

        public string Currency { get; private set; }

        public int ConfirmedCount { get; private set; }

        public int WithdrawnCount { get; private set; }
    }
}