using System;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class PaymentViewModel : IPayment
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string Provider { get; set; }

        public string ProviderKey { get; set; }

        public string InternalReference { get; set; }

        public string ExternalReference { get; set; }

        public DateTime Time { get; set; }

        public PaymentStatus Status { get; set; }

        public DateTime StatusUpdated { get; set; }

        public bool IsTest { get; set; }
    }
}