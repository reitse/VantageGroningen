using System;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Workflows
{
    public class ClubsWorkflow : IDisposable
    {
        private readonly IVantageContext context;
        private bool isDisposed;

        public ClubsWorkflow(IVantageContext context)
        {
            this.context = context;
        }

        public IQueryable<Club> Clubs => context.Clubs;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        #endregion

        public async Task AddClubAsync(Club club)
        {
            context.Clubs.Add(club);
            await context.SaveChangesAsync();
        }

        public async Task UpdateClubAsync(Club club)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteClubAsync(Club club)
        {
            context.Clubs.Remove(club);
            await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    context.Dispose();

                isDisposed = true;
            }
        }

        ~ClubsWorkflow()
        {
            Dispose(false);
        }
    }
}