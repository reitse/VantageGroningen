namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class PaymentDetailsViewModel : PaymentViewModel
    {
        public CompetitionRegistrationViewModel[] CompetitionRegistrations { get; set; }

        public CompetitionSerieRegistrationViewModel[] SerieRegistrations { get; set; }

        public LicensePurchaseViewModel[] LicensePurchases { get; set; }
    }
}