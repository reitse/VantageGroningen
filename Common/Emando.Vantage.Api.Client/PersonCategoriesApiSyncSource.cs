using System.Collections.Generic;
using System.Threading;
using Emando.Vantage.Components.Sync;

namespace Emando.Vantage.Api.Client
{
    public class PersonCategoriesApiSyncSource : ISyncSource<IPersonCategory>
    {
        private readonly PersonCategoriesApiClient client;

        public PersonCategoriesApiSyncSource(PersonCategoriesApiClient client)
        {
            this.client = client;
        }

        public string Source => client.BaseUri.ToString();

        #region ISyncSource<IPersonCategory> Members

        public IEnumerable<IPersonCategory> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            return client.GetCategoriesAsync(cancellationToken).Result;
        }

        #endregion
    }
}