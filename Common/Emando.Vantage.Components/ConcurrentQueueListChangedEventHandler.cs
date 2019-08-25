using System;
using System.Collections.Generic;

namespace Emando.Vantage.Components
{
    public delegate void ConcurrentQueueListChangedEventHandler<T>(object sender, ConcurrentQueueListChangedEventArgs<T> e);

    public class ConcurrentQueueListChangedEventArgs<T> : EventArgs
    {
        public ConcurrentQueueListChangedEventArgs(IReadOnlyCollection<T> list)
        {
            this.List = list;
        }

        public IReadOnlyCollection<T> List { get; }
    }
}