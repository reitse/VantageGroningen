using System;
using System.Threading.Tasks;

namespace Emando.Vantage.Components
{
    public interface IRedirectPaymentProvider : IPaymentProvider
    {
        Task<Uri> StartRedirectAsync(IPayment payment);
    }
}