using System;
using Emando.Vantage.Competitions.Registrations;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class CompetitionSerieRegistrationViewModel
    {
        public Guid Id { get; set; }

        public Guid SerieId { get; set; }

        public string SerieName { get; set; }

        public PersonLicenseViewModel License { get; set; }

        public Guid? PaymentId { get; set; }

        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public DateTime Time { get; set; }

        public RegistrationStatus Status { get; set; }
    }
}