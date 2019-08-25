using System;
using System.Threading.Tasks;

namespace Emando.Vantage
{
    public interface ITransponderScanner : IObservable<ITransponderScan>, IDisposable
    {
        void Start();

        void Stop();

        Task DisconnectAsync();
    }
}