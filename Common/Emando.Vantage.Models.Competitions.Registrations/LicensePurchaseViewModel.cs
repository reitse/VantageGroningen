using System;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class LicensePurchaseViewModel
    {
        public Guid Id { get; set; }

        public PersonLicenseViewModel License { get; set; }

        public Guid? PaymentId { get; set; }

        public string Currency { get; set; }

        public decimal? Amount { get; set; }

        public DateTime Time { get; set; }

        public int Season { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}