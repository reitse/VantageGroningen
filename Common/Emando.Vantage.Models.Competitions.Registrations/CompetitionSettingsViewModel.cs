using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class CompetitionSettingsViewModel
    {
        public DateTime Opens { get; set; }

        public DateTime Closes { get; set; }

        public DateTime WithdrawUntil { get; set; }

        public bool IsClosed { get; set; }

        public int? MaxCompetitors { get; set; }

        public CompetitorListGrouping CompetitorListGrouping { get; set; }

        public bool AllowMultipleCombinations { get; set; }

        public DistanceCombinationSettingsViewModel[] DistanceCombinations { get; set; }

        public string PaymentProvider { get; set; }

        public string PaymentProviderKey { get; set; }

        public string PaymentReference { get; set; }

        public string Extra { get; set; }

        public string Currency { get; set; }

        public ContactViewModel Contact { get; set; }

        public string InvitationIntroduction { get; set; }

        public string InvitationFooter { get; set; }

        public string TemporaryLicenseSecret { get; set; }
    }
}