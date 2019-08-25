using System;

namespace Emando.Vantage.Server.Services
{
    public interface IServiceHost : IDisposable
    {
        Uri BaseAddress { get; }

        void Open();

        void Close();
    }
}