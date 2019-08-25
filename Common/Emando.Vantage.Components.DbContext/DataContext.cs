using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Emando.Vantage.Components
{
    public abstract class DataContext : DbContext, IDataContext
    {
        private IContextTransaction transaction;

        protected DataContext()
        {
            LazyLoadingEnabled = false;
            ProxyCreationEnabled = false;
        }

        protected DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            LazyLoadingEnabled = false;
            ProxyCreationEnabled = false;
        }

        protected DataContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
            LazyLoadingEnabled = false;
            ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            base.OnModelCreating(modelBuilder);
        }

        #region IDataContext Members

        public bool LazyLoadingEnabled
        {
            get { return Configuration.LazyLoadingEnabled; }
            set { Configuration.LazyLoadingEnabled = value; }
        }

        public bool ProxyCreationEnabled
        {
            get { return Configuration.ProxyCreationEnabled; }
            set { Configuration.ProxyCreationEnabled = value; }
        }

        public bool AutoDetectChangesEnabled
        {
            get { return Configuration.AutoDetectChangesEnabled; }
            set { Configuration.AutoDetectChangesEnabled = value; }
        }

        public void Initialize(bool force)
        {
            Database.Initialize(force);
        }

        public IContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            transaction = new DbContextTransactionProxy(Database.BeginTransaction(isolationLevel));
            return transaction;
        }

        public IContextTransaction UseOrBeginTransaction(IsolationLevel isolationLevel)
        {
            return transaction != null
                ? new NestedContextTransaction(transaction)
                : BeginTransaction(isolationLevel);
        }

        public Task LoadAsync<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity: class
            where TProperty: class
        {
            return Entry(entity).Reference(navigationProperty).LoadAsync();
        }

        public Task LoadAsync<TEntity, TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty)
            where TEntity : class
            where TElement : class
        {
            return LoadOrEmptyAsync(Entry(entity).Collection(navigationProperty).Query(), entity, navigationProperty.Body as MemberExpression);
        }

        public Task LoadAsync<TEntity, TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty,
            Expression<Func<TElement, bool>> predicate)
            where TEntity : class
            where TElement : class
        {
            return LoadOrEmptyAsync(Entry(entity).Collection(navigationProperty).Query().Where(predicate), entity, navigationProperty.Body as MemberExpression);
        }

        private static async Task LoadOrEmptyAsync<TEntity, TElement>(IQueryable<TElement> query, TEntity entity, MemberExpression memberExpression)
            where TEntity : class
            where TElement : class
        {
            await query.LoadAsync();

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo != null && propertyInfo.GetValue(entity) == null)
                propertyInfo.SetValue(entity, Activator.CreateInstance<Collection<TElement>>());
        }

        #endregion
    }
}