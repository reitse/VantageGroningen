using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.Test
{
    internal class MemoryDbAsyncQueryProvider<T> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider inner;

        internal MemoryDbAsyncQueryProvider(IQueryProvider inner)
        {
            this.inner = inner;
        }

        #region IDbAsyncQueryProvider Members

        public IQueryable CreateQuery(Expression expression)
        {
            return new MemoryDbAsyncEnumerable<T>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MemoryDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }

        #endregion
    }
}