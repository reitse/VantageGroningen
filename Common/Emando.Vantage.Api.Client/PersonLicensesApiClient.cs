using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Api.Models;
using Emando.Vantage.Models;

namespace Emando.Vantage.Api.Client
{
    public class PersonLicensesApiClient : ApiClient
    {
        public PersonLicensesApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public PersonLicensesApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<PersonLicenseDetailsViewModel[]> GetLicensesAsync(string issuerId, string discipline = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string relativeUri = $"people/licenses/{issuerId}";
            if (discipline != null)
                relativeUri += $"/{discipline}";

            return GetAsAsync<PersonLicenseDetailsViewModel[]>(relativeUri, cancellationToken);
        }

        public Task<PersonLicenseDetailsViewModel> GetLicenseAsync(string issuerId, string discipline, string key,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PersonLicenseDetailsViewModel>($"people/licenses/{issuerId}/{discipline}/{key}", cancellationToken);
        }

        public Task<PersonCategoryViewModel> GetCategoryAsync(string issuerId, string discipline, Gender gender, DateTime birthDate, DateTime? reference = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PersonCategoryViewModel>(
                $"people/licenses/{issuerId}/{discipline}/category/{(int)gender}/{birthDate.ToString("yyyy-MM-dd")}/{reference?.ToString("yyyy-MM-dd")}", cancellationToken);
        }

        public async Task<PersonLicenseDetailsViewModel> UpdateLicenseAsync(string issuerId, string discipline, string key, PersonLicenseBindingModel model,
            CancellationToken cancellationToken)
        {
            var relativeUri = $"people/licenses/{issuerId}/{discipline}/{key}";
            return await PutAsync<PersonLicenseBindingModel, PersonLicenseDetailsViewModel>(relativeUri, model, cancellationToken);
        }
    }
}