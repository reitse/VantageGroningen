using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Emando.Vantage.Components
{
    public interface IDataContext : IContextInitializer, IDisposable
    {
        bool LazyLoadingEnabled { get; set; }

        bool ProxyCreationEnabled { get; set; }

        bool AutoDetectChangesEnabled { get; set; }

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IContextTransaction BeginTransaction(IsolationLevel isolationLevel);

        IContextTransaction UseOrBeginTransaction(IsolationLevel isolationLevel);

        Task LoadAsync<TEntity, TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty)
            where TEntity : class
            where TElement : class;

        Task LoadAsync<TEntity, TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty, Expression<Func<TElement, bool>> predicate)
            where TEntity : class
            where TElement : class;

        Task LoadAsync<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity: class
            where TProperty: class;
    }
}