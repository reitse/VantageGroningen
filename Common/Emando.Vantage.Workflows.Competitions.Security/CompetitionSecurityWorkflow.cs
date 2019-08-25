using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Workflows.Security;

namespace Emando.Vantage.Workflows.Competitions.Security
{
    public class CompetitionSecurityWorkflow : IDisposable
    {
        private readonly ICompetitionContext context;
        private readonly UserSecurityWorkflow userSecurityWorkflow;
        private bool isDisposed;

        public CompetitionSecurityWorkflow(ICompetitionContext context)
        {
            this.context = context;
            userSecurityWorkflow = new UserSecurityWorkflow(context);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public async Task<bool> AllowChangeAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var competition = await (from c in context.Competitions
                                     where c.Id == competitionId
                                     select new
                                     {
                                         c.ResultsStatus
                                     }).FirstOrDefaultAsync(cancellationToken);
            return competition?.ResultsStatus != CompetitionResultsStatus.Official;
        }

        public async Task<bool> IsInAnyRoleAsync(ClaimsIdentity identity, string[] roles, Guid competitionId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var competition = await (from c in context.Competitions
                                     where c.Id == competitionId
                                     select new
                                     {
                                         c.LicenseIssuerId,
                                         c.Discipline,
                                         c.SerieId,
                                         c.Class,
                                         c.VenueCode
                                     }).FirstOrDefaultAsync(cancellationToken);
            if (competition == null)
                return false;

            if (competition.VenueCode == null)
                return true;

            return await userSecurityWorkflow.IsInAnyRoleAsync(identity, roles, competition.LicenseIssuerId, competition.Discipline, competition.SerieId,
                competition.VenueCode, competition.Discipline, competition.Class, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    userSecurityWorkflow.Dispose();
                    context.Dispose();
                }

                isDisposed = true;
            }
        }

        ~CompetitionSecurityWorkflow()
        {
            Dispose(false);
        }
    }
}