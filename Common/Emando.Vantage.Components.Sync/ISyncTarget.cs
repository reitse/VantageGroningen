using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.Sync
{
    public interface ISyncTarget<T> : ISyncSource<T>
    {
        string Target { get; }

        bool CanDelete(T item);

        Task DeleteAsync(IEnumerable<T> items, CancellationToken cancellationToken = default(CancellationToken));

        bool CanUpdate(T item);

        Task UpdateAsync(IEnumerable<T> items, CancellationToken cancellationToken);

        bool CanInsert(T item);

        Task InsertAsync(IEnumerable<T> items, CancellationToken cancellationToken = default(CancellationToken));
    }
}