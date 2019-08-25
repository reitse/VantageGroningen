using System;

namespace Emando.Vantage.Competitions.Registrations
{
    public class CompetitionRegistrationsStatistics
    {
        public CompetitionRegistrationsStatistics(Guid id, string name, DateTime starts, string currency, decimal? amount, int confirmedCount, int withdrawnCount)
        {
            Id = id;
            Name = name;
            Starts = starts;
            Amount = amount;
            Currency = currency;
            ConfirmedCount = confirmedCount;
            WithdrawnCount = withdrawnCount;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime Starts { get; private set; }

        public decimal? Amount { get; private set; }

        public string Currency { get; private set; }

        public int ConfirmedCount { get; private set; }

        public int WithdrawnCount { get; private set; }
    }
}