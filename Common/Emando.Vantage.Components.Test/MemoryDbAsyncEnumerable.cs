using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Emando.Vantage.Components.Test
{
    internal class MemoryDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>
    {
        public MemoryDbAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
        {
        }

        public MemoryDbAsyncEnumerable(Expression expression) : base(expression)
        {
        }

        public IQueryProvider Provider => new MemoryDbAsyncQueryProvider<T>(this);

        #region IDbAsyncEnumerable<T> Members

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new MemoryDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        #endregion
    }
}