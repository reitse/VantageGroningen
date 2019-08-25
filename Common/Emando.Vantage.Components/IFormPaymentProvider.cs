using System;
using System.Collections.Specialized;

namespace Emando.Vantage.Components
{
    public interface IFormPaymentProvider : IPaymentProvider
    {
        Uri FormAction { get; }

        NameValueCollection ComputeFormFields(IPayment payment, string language = null);

        bool TryGetStatus(NameValueCollection formFields, out Guid id, out PaymentStatus status, out string externalReference);
    }
}