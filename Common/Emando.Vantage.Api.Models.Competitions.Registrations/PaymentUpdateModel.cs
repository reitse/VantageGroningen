namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class PaymentUpdateModel
    {
        public PaymentStatus Status { get; set; }

        public string ExternalReference { get; set; }
    }
}