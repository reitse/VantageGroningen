namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class CompetitionSerieSettingsViewModel
    {
        public CompetitionSerieCategorySettingsViewModel[] Categories { get; set; }

        public string PaymentProvider { get; set; }

        public string PaymentProviderKey { get; set; }

        public string PaymentReference { get; set; }

        public bool RequireSerieRegistration { get; set; }

        public string Extra { get; set; }

        public string Currency { get; set; }

        public ContactViewModel Contact { get; set; }
    }
}