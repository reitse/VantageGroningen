using System.Threading.Tasks;

namespace Emando.Vantage.Components
{
    public interface IStatusPaymentProvider : IPaymentProvider
    {
        Task<PaymentStatus> GetStatusAsync(IPayment payment);
    }
}