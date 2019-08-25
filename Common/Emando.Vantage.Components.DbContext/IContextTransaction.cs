using System;

namespace Emando.Vantage.Components
{
    public interface IContextTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}