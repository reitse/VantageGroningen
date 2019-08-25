using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Models;

namespace Emando.Vantage.Api.Client
{
    public class TranspondersApiClient : ApiClient
    {
        public TranspondersApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public TranspondersApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<TransponderSetViewModel[]> GetSetsAsync(string licenseIssuerId, string discipline, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<TransponderSetViewModel[]>($"transponders/sets/{licenseIssuerId}/{discipline}", cancellationToken);
        }

        public Task<TransponderSetViewModel[]> GetBagAsync(string licenseIssuerId, string discipline, string name,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<TransponderSetViewModel[]>($"transponders/bags/{licenseIssuerId}/{discipline}/{name}/sets", cancellationToken);
        }

        public Task<TransponderSetViewModel[]> ResetBagAsync(string licenseIssuerId, string discipline, string name, int[] sets,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostAsync<int[], TransponderSetViewModel[]>($"transponders/bags/{licenseIssuerId}/{discipline}/{name}/sets", sets, cancellationToken);
        }
    }
}