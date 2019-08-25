using System.Collections.Generic;
using System.Threading;

namespace Emando.Vantage.Components.Sync
{
    public interface ISyncSource<out T>
    {
        string Source { get; }

        IEnumerable<T> Extract(CancellationToken cancellationToken = default(CancellationToken));
    }
}