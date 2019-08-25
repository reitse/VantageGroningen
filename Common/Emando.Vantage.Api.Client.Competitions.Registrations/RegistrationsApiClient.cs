using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Api.Models.Competitions.Registrations;
using Emando.Vantage.Models.Competitions.Registrations;

namespace Emando.Vantage.Api.Client.Competitions.Registrations
{
    public class RegistrationsApiClient : CompetitionsApiClient
    {
        public RegistrationsApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public RegistrationsApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<CompetitionWithRegistrationViewModel[]> GetCompetitionsWithRegistrationAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionWithRegistrationViewModel[]>("competitions/with-registration", cancellationToken);
        }

        public Task<CompetitionWithRegistrationViewModel> GetCompetitionWithRegistrationAsync(Guid competitionId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionWithRegistrationViewModel>($"competitions/with-registration/{competitionId}", cancellationToken);
        }

        public Task<CompetitionSettingsViewModel> GetCompetitionSettingsAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionSettingsViewModel>($"competitions/{competitionId}/registration/settings", cancellationToken);
        }

        public Task<DistanceCombinationRegistrationSettingsViewModel[]> GetDistanceCombinationSettingsAsync(Guid competitionId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<DistanceCombinationRegistrationSettingsViewModel[]>($"competitions/{competitionId}/registration/settings/distancecombinations",
                cancellationToken);
        }

        public Task<DistanceCombinationRegistrationSettingsViewModel[]> GetDistanceCombinationSettingsAsync(Guid competitionId, string category, int? @class = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<DistanceCombinationRegistrationSettingsViewModel[]>(
                $"competitions/{competitionId}/registration/settings/distancecombinations/{category}/{@class}",
                cancellationToken);
        }

        public Task<LicenseRegistrationSettingsViewModel> GetLicenseSettingsAsync(Guid competitionId, string key,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<LicenseRegistrationSettingsViewModel>($"competitions/{competitionId}/registration/settings/{key}", cancellationToken);
        }

        public Task<CompetitionRegistrationViewModel[]> GetRegistrationsAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionRegistrationViewModel[]>($"competitions/{competitionId}/registrations", cancellationToken);
        }

        public Task<CompetitionRegistrationViewModel> GetRegistrationAsync(Guid competitionId, Guid registrationId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionRegistrationViewModel>($"competitions/{competitionId}/registrations/{registrationId}", cancellationToken);
        }

        public Task<CompetitionRegistrationViewModel> RegisterAsync(Guid competitionId, string key, RegisterModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostAsync<RegisterModel, CompetitionRegistrationViewModel>($"competitions/{competitionId}/registrations/{key}",
                model, cancellationToken);
        }

        public Task<CompetitionRegistrationViewModel> RegisterWithNewTemporaryLicenseAsync(Guid competitionId,
            RegisterWithNewTemporaryLicenseModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostAsync<RegisterWithNewTemporaryLicenseModel, CompetitionRegistrationViewModel>($"competitions/{competitionId}/registrations/temporary",
                model, cancellationToken);
        }

        public Task<CompetitionRegistrationViewModel> CompleteRegistrationAsync(Guid competitionId, Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PutAsync<CompetitionRegistrationViewModel>($"competitions/{competitionId}/registrations/{id}/complete", cancellationToken);
        }

        public Task WithdrawRegistrationAsync(Guid competitionId, Guid registrationId, WithdrawModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PutAsync($"competitions/{competitionId}/registrations/{registrationId}/withdraw", model, cancellationToken);
        }

        public Task<PaymentDetailsViewModel> GetPaymentAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PaymentDetailsViewModel>($"competitions/registration/payments/{id}", cancellationToken);
        }

        public Task<PaymentDetailsViewModel> UpdatePaymentAsync(Guid id, PaymentUpdateModel update, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PutAsync<PaymentUpdateModel, PaymentDetailsViewModel>($"competitions/registration/payments/{id}", update, cancellationToken);
        }
    }
}