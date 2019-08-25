using System;
using System.Collections.Generic;
using System.Threading;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Sync;

namespace Emando.Vantage.Api.Client.Competitions
{
    public class PersonTimeApiSyncSource : ISyncSource<IPersonLicenseTime>
    {
        private readonly PersonTimesApiClient client;

        public PersonTimeApiSyncSource(PersonTimesApiClient client)
        {
            this.client = client;
        }

        #region ISyncSource<IPersonLicenseTime> Members

        public string Source => client.BaseUri.ToString();

        public IEnumerable<IPersonLicenseTime> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            return client.GetPersonTimes(cancellationToken);
        }

        #endregion
    }
}