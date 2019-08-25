using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Workflows
{
    public class PeopleWorkflow : IDisposable
    {
        private readonly IVantageContext context;
        private bool isDisposed;

        public PeopleWorkflow(IVantageContext context)
        {
            this.context = context;
        }

        public IQueryable<Person> People => context.Persons;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    context.Dispose();
                isDisposed = true;
            }
        }

        ~PeopleWorkflow()
        {
            Dispose(false);
        }
    }
}