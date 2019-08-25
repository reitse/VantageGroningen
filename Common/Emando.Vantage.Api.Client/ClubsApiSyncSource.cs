using System;
using System.Collections.Generic;
using System.Threading;
using Emando.Vantage.Components.Sync;

namespace Emando.Vantage.Api.Client
{
    public class ClubsApiSyncSource : ISyncSource<IClub>
    {
        private readonly ClubsApiClient client;

        public ClubsApiSyncSource(ClubsApiClient client)
        {
            this.client = client;
        }

        #region ISyncSource<IClub> Members

        public string Source => client.BaseUri.ToString();

        public IEnumerable<IClub> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            return client.GetClubsAsync(cancellationToken).Result;
        }

        #endregion
    }
}