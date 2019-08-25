using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Emando.Vantage.Components.Sync;
using Emando.Vantage.Models;

namespace Emando.Vantage.Api.Client
{
    public class PeopleApiSyncSource : ISyncSource<IPerson>, ISyncSource<IPersonLicense>
    {
        private readonly PeopleApiClient client;
        private readonly bool enableCache;
        private IReadOnlyList<PersonDetailsViewModel> cache;

        public PeopleApiSyncSource(PeopleApiClient client, bool enableCache = false)
        {
            this.client = client;
            this.enableCache = enableCache;
        }

        public string Source => client.BaseUri.ToString();

        #region ISyncSource<IPerson> Members

        public IEnumerable<IPerson> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            return enableCache && cache != null ? cache : cache = GetFromSourceAsync(cancellationToken);
        }

        #endregion

        #region ISyncSource<IPersonLicense> Members

        IEnumerable<IPersonLicense> ISyncSource<IPersonLicense>.Extract(CancellationToken cancellationToken)
        {
            var people = enableCache && cache != null ? cache : cache = GetFromSourceAsync(cancellationToken);
            return people.SelectMany(p => p.Licenses);
        }

        #endregion

        private IReadOnlyList<PersonDetailsViewModel> GetFromSourceAsync(CancellationToken cancellationToken)
        {
            return client.Get(cancellationToken).ToList();
        }
    }
}