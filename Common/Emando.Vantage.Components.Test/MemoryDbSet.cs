using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Emando.Vantage.Components.Test
{
    public class MemoryDbSet<T> : DbSet<T>, IDbAsyncEnumerable<T>, IEnumerable<T>, IQueryable
        where T : class
    {
        private readonly ObservableCollection<T> data;
        private readonly IQueryable query;

        public MemoryDbSet()
        {
            data = new ObservableCollection<T>();
            query = new MemoryDbAsyncEnumerable<T>(data.AsQueryable());
        }

        public override ObservableCollection<T> Local => data;

        #region IDbAsyncEnumerable<T> Members

        IDbAsyncEnumerator<T> IDbAsyncEnumerable<T>.GetAsyncEnumerator()
        {
            return new MemoryDbAsyncEnumerator<T>(data.GetEnumerator());
        }

        #endregion

        public override T Find(params object[] keyValues)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
            var extractKeys = new Func<T, IEnumerable<object>>(e => (from p in e.GetType().GetProperties(bindingFlags)
                                                                     let key = p.GetCustomAttribute<KeyAttribute>()
                                                                     where key != null
                                                                     let column = p.GetCustomAttribute<ColumnAttribute>()
                                                                     let order = column != null ? column.Order : 0
                                                                     orderby order
                                                                     select p.GetValue(e)));
            return data.FirstOrDefault(e => extractKeys(e).SequenceEqual(keyValues));
        }

        public override T Add(T item)
        {
            data.Add(item);
            return item;
        }

        public override T Remove(T item)
        {
            data.Remove(item);
            return item;
        }

        public override T Attach(T item)
        {
            data.Add(item);
            return item;
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        #region IEnumerable<T> Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        Type IQueryable.ElementType => query.ElementType;

        Expression IQueryable.Expression => query.Expression;

        IQueryProvider IQueryable.Provider => new MemoryDbAsyncQueryProvider<T>(query.Provider);

        #endregion
    }
}