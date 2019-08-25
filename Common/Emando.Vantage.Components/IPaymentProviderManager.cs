namespace Emando.Vantage.Components
{
    public interface IPaymentProviderManager
    {
        T Find<T>(string type) where T : IPaymentProvider;
    }
}