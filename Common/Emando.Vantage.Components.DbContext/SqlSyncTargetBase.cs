using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Emando.Vantage.Components.Sync;

namespace Emando.Vantage.Components
{
    public abstract class SqlSyncTargetBase<T> : ISyncTarget<T>, IDisposable
    {
        private const int LogTimes = 1000;
        private readonly ILog log;
        private readonly SqlSyncSourceBase<T> source;
        private bool isDisposed;

        protected SqlSyncTargetBase(SqlSyncSourceBase<T> source)
        {
            this.source = source;
            log = LogManager.GetLogger(GetType());
        }

        protected SqlConnection Connection => source.Connection;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region ISyncTarget<T> Members

        public string Source => source.Source;

        public IEnumerable<T> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            return source.Extract(cancellationToken);
        }

        public string Target => "Vantage";

        public virtual bool CanDelete(T item)
        {
            return true;
        }

        public async Task DeleteAsync(IEnumerable<T> items, CancellationToken cancellationToken = new CancellationToken())
        {
            if (Connection.State == ConnectionState.Closed)
                await Connection.OpenAsync(cancellationToken);

            using (var command = CreateDeleteCommand(Connection))
            {
                command.Prepare();

                var count = 0;
                foreach (var item in items.Where(CanDelete))
                {
                    SetDeleteParameters(command, item);

                    try
                    {
                        await command.ExecuteNonQueryAsync(cancellationToken);

                        count++;
                        if (count % LogTimes == 0)
                        {
                            var c = count;
                            log.Info(l => l("Deleted {0} items.", c));
                        }
                    }
                    catch (Exception e)
                    {
                        log.Warn(l => l("Failed to delete item {0}: {1}", item, e.InnerMost().Message));
                    }
                }
                log.Info(l => l("Deleted {0} items.", count));
            }
        }

        public virtual bool CanUpdate(T item)
        {
            return true;
        }

        public async Task UpdateAsync(IEnumerable<T> items, CancellationToken cancellationToken)
        {
            if (Connection.State == ConnectionState.Closed)
                await Connection.OpenAsync(cancellationToken);

            using (var command = CreateUpdateCommand(Connection))
            {
                command.Prepare();

                var count = 0;
                foreach (var item in items.Where(CanUpdate))
                {
                    SetUpdateParameters(command, item);

                    try
                    {
                        await command.ExecuteNonQueryAsync(cancellationToken);

                        count++;
                        if (count % LogTimes == 0)
                        {
                            var c = count;
                            log.Info(l => l("Updated {0} items.", c));
                        }
                    }
                    catch (Exception e)
                    {
                        log.Warn(l => l("Failed to update item {0}: {1}", item, e.InnerMost().Message));
                    }
                }
                log.Info(l => l("Updated {0} items.", count));
            }
        }

        public virtual bool CanInsert(T item)
        {
            return true;
        }

        protected virtual Task OnBeforeInsertAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult<object>(null);
        }

        public async Task InsertAsync(IEnumerable<T> items, CancellationToken cancellationToken = new CancellationToken())
        {
            if (Connection.State == ConnectionState.Closed)
                await Connection.OpenAsync(cancellationToken);

            using (var command = CreateInsertCommand(Connection))
            {
                command.Prepare();

                var count = 0;
                foreach (var item in items.Where(CanInsert))
                {
                    SetInsertParameters(command, item);
                    try
                    {
                        await command.ExecuteNonQueryAsync(cancellationToken);

                        count++;
                        if (count % LogTimes == 0)
                        {
                            var c = count;
                            log.Info(l => l("Inserted {0} items.", c));
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(l => l("Failed to insert item {0}: {1}", item, e.InnerMost().Message));
                    }
                }
                log.Info(l => l("Inserted {0} items.", count));
            }
        }

        #endregion

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    source.Dispose();

                isDisposed = true;
            }
        }

        ~SqlSyncTargetBase()
        {
            Dispose(false);
        }

        protected abstract SqlCommand CreateDeleteCommand(SqlConnection connection);

        protected abstract void SetDeleteParameters(SqlCommand command, T item);

        protected abstract SqlCommand CreateUpdateCommand(SqlConnection connection);

        protected abstract void SetUpdateParameters(SqlCommand command, T item);

        protected abstract SqlCommand CreateInsertCommand(SqlConnection connection);

        protected abstract void SetInsertParameters(SqlCommand command, T item);
    }
}