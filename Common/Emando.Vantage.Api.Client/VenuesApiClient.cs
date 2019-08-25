using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Models;

namespace Emando.Vantage.Api.Client
{
    public class VenuesApiClient : ApiClient
    {
        public VenuesApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public VenuesApiClient(Uri baseUri, string clientId, SecureString clientSecret = null)
            : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<VenueViewModel[]> GetVenuesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<VenueViewModel[]>("venues", cancellationToken);
        }
    }
}