using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Emando.Vantage.Components
{
    public class ConcurrentQueueList<T> : IDisposable
    {
        private bool isDisposed;

        protected List<T> Items { get; } = new List<T>();

        protected ReaderWriterLockSlim Locker { get; } = new ReaderWriterLockSlim();

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public event ConcurrentQueueListChangedEventHandler<T> Changed;

        protected virtual void OnChanged()
        {
            var handler = Changed;
            handler?.Invoke(this, new ConcurrentQueueListChangedEventArgs<T>(Items.AsReadOnly()));
        }

        protected virtual void OnCleared()
        {
        }

        public T Peek()
        {
            Locker.EnterReadLock();
            try
            {
                return Items.FirstOrDefault();
            }
            finally
            {
                Locker.ExitReadLock();
            }
        }

        public bool Contains(T item)
        {
            Locker.EnterReadLock();
            try
            {
                return Items.Contains(item);
            }
            finally
            {
                Locker.ExitReadLock();
            }
        }

        public List<T> ToList()
        {
            Locker.EnterReadLock();
            try
            {
                return Items.ToList();
            }
            finally
            {
                Locker.ExitReadLock();
            }
        }

        public void Enqueue(T item)
        {
            Locker.EnterWriteLock();
            try
            {
                EnqueueCore(item);
            }
            finally
            {
                Locker.ExitWriteLock();
            }
        }

        protected virtual int EnqueueCore(T item)
        {
            if (!Locker.IsWriteLockHeld)
                throw new InvalidOperationException();

            Items.Remove(item);
            Items.Add(item);
            OnChanged();
            return Items.Count - 1;
        }

        protected virtual void EnqueueAtCore(int index, T item)
        {
            if (!Locker.IsWriteLockHeld)
                throw new InvalidOperationException();

            Items.Remove(item);
            Items.Insert(Math.Min(index, Items.Count), item);
            OnChanged();
        }

        public void EnqueueBefore(Predicate<T> predicate, T item)
        {
            Locker.EnterWriteLock();
            try
            {
                EnqueueBeforeCore(predicate, item);
            }
            finally
            {
                Locker.ExitWriteLock();
            }
        }

        protected virtual int EnqueueBeforeCore(Predicate<T> predicate, T item)
        {
            var matchIndex = Items.FindIndex(predicate);
            var index = matchIndex != -1 ? matchIndex : Items.Count;
            EnqueueAtCore(index, item);
            return index;
        }

        public void EnqueueAt(int index, T item)
        {
            Locker.EnterWriteLock();
            try
            {
                EnqueueAtCore(index, item);
            }
            finally
            {
                Locker.ExitWriteLock();
            }
        }

        public bool Dequeue(T item)
        {
            Locker.EnterWriteLock();
            try
            {
                if (Items.Remove(item))
                {
                    OnDequeued(item);
                    OnChanged();
                    return true;
                }
                return false;
            }
            finally
            {
                Locker.ExitWriteLock();
            }
        }

        protected virtual void OnDequeued(T item)
        {
        }

        public void Clear()
        {
            Locker.EnterWriteLock();
            try
            {
                Items.Clear();
                OnCleared();
                OnChanged();
            }
            finally
            {
                Locker.ExitWriteLock();
            }
        }

        ~ConcurrentQueueList()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    Locker.Dispose();

                isDisposed = true;
            }
        }

        public int IndexOf(T item)
        {
            Locker.EnterReadLock();
            try
            {
                return Items.IndexOf(item);
            }
            finally
            {
                Locker.ExitReadLock();
            }
        }
    }
}