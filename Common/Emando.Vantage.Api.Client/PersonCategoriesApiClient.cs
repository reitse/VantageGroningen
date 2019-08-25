using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Models;

namespace Emando.Vantage.Api.Client
{
    public class PersonCategoriesApiClient : ApiClient
    {
        public PersonCategoriesApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public PersonCategoriesApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<PersonCategoryViewModel[]> GetCategoriesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PersonCategoryViewModel[]>("people/categories", cancellationToken);
        }

        public Task<PersonCategoryViewModel[]> GetCategoriesAsync(string issuerId, string discipline, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PersonCategoryViewModel[]>($"people/categories/{issuerId}/{discipline}", cancellationToken);
        }
    }
}