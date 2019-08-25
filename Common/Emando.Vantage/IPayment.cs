using System;

namespace Emando.Vantage
{
    public interface IPayment
    {
        Guid Id { get; }

        string Currency { get; }

        decimal Amount { get; }

        string Provider { get; }

        string ProviderKey { get; }

        string InternalReference { get; }

        string ExternalReference { get; set; }

        DateTime Time { get; }

        PaymentStatus Status { get; set; }

        DateTime StatusUpdated { get; set; }

        bool IsTest { get; }
    }
}