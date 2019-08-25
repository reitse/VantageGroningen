using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Identity;

namespace Emando.Vantage.Workflows.Security
{
    public class UserSecurityWorkflow : IDisposable
    {
        private readonly IVantageContext context;
        private bool isDisposed;

        public UserSecurityWorkflow(IVantageContext context)
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

        public async Task<bool> IsInAnyRoleAsync(ClaimsIdentity identity, string[] roles, string licenseIssuerId, string licenseDiscipline, Guid? serieId, string venueCode,
            string venueDiscipline, int competitionClass, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (identity == null)
                return false;

            var venue = await context.Venues.Include(v => v.Districts).FirstOrDefaultAsync(v => v.Code == venueCode && v.Discipline == venueDiscipline, cancellationToken);
            if (venue == null)
                return false;

            var hasRight = new Func<int, string, string, bool>((@class, value, role) => competitionClass <= @class
                && identity.HasClaim(VantageClaimTypes.CompetitionRight, new CompetitionRight(licenseIssuerId, licenseDiscipline, @class, value, role).Encode()));

            return roles.Any(role => hasRight(CompetitionClasses.Test, null, role)
                //|| hasRight(CompetitionClasses.Club, competition.ClubCode, role))
                || hasRight(CompetitionClasses.Venue, venueCode, role)
                || venue.Districts.Any(d => d.Level == VenueDistrictLevels.Area && hasRight(CompetitionClasses.Area, d.Code, role))
                || serieId.HasValue && hasRight(CompetitionClasses.Serie, serieId.ToString(), role)
                || hasRight(CompetitionClasses.National, venue.Address.CountryCode, role)
                || hasRight(CompetitionClasses.International, null, role));
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

        ~UserSecurityWorkflow()
        {
            Dispose(false);
        }
    }
}