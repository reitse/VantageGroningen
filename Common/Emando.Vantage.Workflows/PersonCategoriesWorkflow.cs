using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Workflows
{
    public class PersonCategoriesWorkflow : IDisposable
    {
        private readonly IVantageContext context;
        private bool isDisposed;

        public PersonCategoriesWorkflow(IVantageContext context)
        {
            this.context = context;
        }

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

        ~PersonCategoriesWorkflow()
        {
            Dispose(false);
        }

        public IQueryable<PersonCategory> AllCategories => from c in context.PersonCategories
                                                           orderby c.Gender descending, c.FromAge, c.ToAge
                                                           select c;

        public IQueryable<PersonCategory> Categories(string licenseIssuerId, string discipline)
        {
            return from c in AllCategories
                   where c.LicenseIssuerId == licenseIssuerId && discipline.StartsWith(c.Discipline)
                   select c;
        }
    }
}
