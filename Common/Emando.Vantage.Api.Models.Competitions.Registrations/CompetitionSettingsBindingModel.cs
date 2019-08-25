using System;
using System.ComponentModel.DataAnnotations;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class CompetitionSettingsBindingModel
    {
        public DateTime Opens { get; set; }

        public DateTime Closes { get; set; }

        public DateTime WithdrawUntil { get; set; }

        [Range(1, int.MaxValue)]
        public int? MaxCompetitors { get; set; }

        public CompetitorListGrouping CompetitorListGrouping { get; set; }

        public DistanceCombinationSettingsBindingModel[] DistanceCombinations { get; set; }

        public bool AllowMultipleCombinations { get; set; }

        [StringLength(20)]
        public string PaymentProvider { get; set; }

        [StringLength(100)]
        public string PaymentProviderKey { get; set; }

        [StringLength(20)]
        public string PaymentReference { get; set; }

        public string Extra { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; }

        [Required]
        public ContactBindingModel Contact { get; set; }

        public string InvitationIntroduction { get; set; }

        public string InvitationFooter { get; set; }

        public string TemporaryLicenseSecret { get; set; }
    }
}