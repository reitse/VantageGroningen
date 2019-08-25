using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.Test
{
    internal class MemoryDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> inner;

        public MemoryDbAsyncEnumerator(IEnumerator<T> inner)
        {
            this.inner = inner;
        }

        #region IDbAsyncEnumerator<T> Members

        public void Dispose()
        {
            inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(inner.MoveNext());
        }

        public T Current => inner.Current;

        object IDbAsyncEnumerator.Current => Current;

        #endregion
    }
}