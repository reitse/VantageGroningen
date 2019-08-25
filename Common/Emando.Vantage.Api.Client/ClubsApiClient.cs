using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Api.Models;
using Emando.Vantage.Models;

namespace Emando.Vantage.Api.Client
{
    public class ClubsApiClient : ApiClient
    {
        public ClubsApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public ClubsApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<ClubViewModel[]> GetClubsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<ClubViewModel[]>("clubs", cancellationToken);
        }

        public Task<ClubViewModel> AddClubAsync(ClubCreateModel model, CancellationToken cancellationToken)
        {
            return PostAsync<ClubCreateModel, ClubViewModel>("clubs", model, cancellationToken);
        }

        public Task UpdateClubAsync(string countryCode, int code, ClubUpdateModel model, CancellationToken cancellationToken)
        {
            return PutAsync($"clubs/{countryCode}/{code}", model, cancellationToken);
        }
    }
}