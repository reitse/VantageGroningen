using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Api.Models;
using Emando.Vantage.Models.Users;

namespace Emando.Vantage.Api.Client
{
    public class UsersApiClient : ApiClient
    {
        public UsersApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public UsersApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<VantageUserDetailsViewModel> CreateUserAsync(VantageUserCreateModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostAsync<VantageUserCreateModel, VantageUserDetailsViewModel>("users", model, cancellationToken);
        }

        public Task UpdateUserAsync(string userId, VantageUserUpdateModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PutAsync($"users/{userId}", model, cancellationToken);
        }
    }
}