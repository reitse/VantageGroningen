namespace Emando.Vantage.Components
{
    public class NestedContextTransaction : IContextTransaction
    {
        private readonly IContextTransaction transaction;

        public NestedContextTransaction(IContextTransaction transaction)
        {
            this.transaction = transaction;
        }

        #region IContextTransaction Members

        public void Dispose()
        {
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
        }

        #endregion
    }
}