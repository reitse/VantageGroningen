using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Workflows
{
    public class VenuesWorkflow : IDisposable
    {
        private readonly IVantageContext context;
        private bool isDisposed;

        public VenuesWorkflow(IVantageContext context)
        {
            this.context = context;
        }

        public IQueryable<Venue> Venues => context.Venues;

        public IQueryable<VenueDistrict> VenueDistricts => context.VenueDistricts;

        public IQueryable<VenueTrack> VenueTracks => context.VenueTracks;

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
    }
}