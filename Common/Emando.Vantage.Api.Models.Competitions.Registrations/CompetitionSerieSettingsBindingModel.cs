using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class CompetitionSerieSettingsBindingModel
    {
        public CompetitionSerieCategorySettingsBindingModel[] Categories { get; set; }

        [StringLength(20)]
        public string PaymentProvider { get; set; }

        [StringLength(100)]
        public string PaymentProviderKey { get; set; }

        [StringLength(20)]
        public string PaymentReference { get; set; }

        public bool RequireSerieRegistration { get; set; }

        public string Extra { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; }

        public ContactBindingModel Contact { get; set; }
    }
}