using System;

namespace Emando.Vantage.Components.Test
{
    public class MockContextTransaction : IContextTransaction
    {
        private bool committed;
        private bool isDisposed;
        private bool rolledback;

        ~MockContextTransaction()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
                isDisposed = true;
        }

        #region IContextTransaction Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            if (rolledback)
                throw new InvalidOperationException();

            committed = true;
        }

        public void Rollback()
        {
            if (committed)
                throw new InvalidOperationException();

            rolledback = true;
        }

        #endregion
    }
}