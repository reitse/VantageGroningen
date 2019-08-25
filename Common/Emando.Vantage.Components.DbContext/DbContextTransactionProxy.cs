using System.Data.Entity;

namespace Emando.Vantage.Components
{
    public class DbContextTransactionProxy : IContextTransaction
    {
        private readonly DbContextTransaction transaction;

        public DbContextTransactionProxy(DbContextTransaction transaction)
        {
            this.transaction = transaction;
        }

        #region IContextTransaction Members

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Dispose()
        {
            transaction.Dispose();
        }

        #endregion
    }
}