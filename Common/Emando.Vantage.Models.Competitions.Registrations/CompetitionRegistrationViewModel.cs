using System;
using Emando.Vantage.Competitions.Registrations;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class CompetitionRegistrationViewModel
    {
        public Guid Id { get; set; }

        public Guid CompetitionId { get; set; }

        public string CompetitionName { get; set; }

        public CompetitorViewModel Competitor { get; set; }

        public string LicenseIssuerId { get; set; }

        public string LicenseDiscipline { get; set; }

        public string LicenseKey { get; set; }

        public PersonLicenseFlags? LicenseFlags { get; set; }

        public string Email { get; set; }

        public Guid? PaymentId { get; set; }

        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public DateTime Time { get; set; }

        public RegistrationStatus Status { get; set; }

        public DistanceCombinationViewModel[] DistanceCombinations { get; set; }
    }
}