using System;
using System.Reactive.Disposables;

namespace Emando.Vantage.Components.IO
{
    public class IOEventSubscription : IDisposable
    {
        private bool isDisposed;
        private readonly CompositeDisposable handles = new CompositeDisposable();

        public IOEventSubscription(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public CompositeDisposable Handles
        {
            get { return handles; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~IOEventSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    handles.Dispose();
                isDisposed = true;
            }
        }
    }
}