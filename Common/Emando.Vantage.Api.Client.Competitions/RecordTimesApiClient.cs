using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Models.Competitions;

namespace Emando.Vantage.Api.Client.Competitions
{
    public class RecordTimesApiClient : ApiClient
    {
        public RecordTimesApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public RecordTimesApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<RecordTimeViewModel[]> GetRecordTimesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<RecordTimeViewModel[]>("records/times", cancellationToken);
        }
    }
}