using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Sync;

namespace Emando.Vantage.Api.Client.Competitions
{
    public class CompetitionsApiSyncSource : ISyncSource<ICompetition>
    {
        private readonly CompetitionsApiClient client;
        private readonly string licenseIssuerId;
        private readonly bool requireProviderKey;


        public CompetitionsApiSyncSource(CompetitionsApiClient client, string licenseIssuerId = null,
            bool requireProviderKey = false)
        {
            this.client = client;
            this.licenseIssuerId = licenseIssuerId;
            this.requireProviderKey = requireProviderKey;
        }

        public string Source => client.BaseUri.ToString();

        #region ISyncSource<ICompetition> Members

        public IEnumerable<ICompetition> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            var competitions = client.GetCompetitionsAsync(cancellationToken).Result.AsEnumerable();
            if (licenseIssuerId != null)
                competitions = competitions.Where(c => c.LicenseIssuerId == licenseIssuerId);
            if (requireProviderKey)
                competitions = competitions.Where(c => c.ProviderKey != null);
            return competitions;
        }

        #endregion
    }
}