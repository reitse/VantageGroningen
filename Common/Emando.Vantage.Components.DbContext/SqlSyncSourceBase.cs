using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Common.Logging;
using Emando.Vantage.Components.Sync;

namespace Emando.Vantage.Components
{
    public abstract class SqlSyncSourceBase<T> : ISyncSource<T>, IDisposable
    {
        private readonly ILog log;
        private bool isDisposed;

        protected SqlSyncSourceBase(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            log = LogManager.GetLogger(GetType());
        }

        protected internal SqlConnection Connection { get; }

        public virtual string Source => "Vantage";

        public IEnumerable<T> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            using (var command = CreateSelectCommand(Connection))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        yield return Read(reader);
                    }
        }

        protected abstract SqlCommand CreateSelectCommand(SqlConnection connection);

        protected abstract T Read(SqlDataReader reader);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    Connection.Dispose();

                isDisposed = true;
            }
        }

        ~SqlSyncSourceBase()
        {
            Dispose(false);
        }
    }
}