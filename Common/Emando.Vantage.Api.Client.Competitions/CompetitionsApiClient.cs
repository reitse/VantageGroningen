using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Api.Models.Competitions;
using Emando.Vantage.Models;
using Emando.Vantage.Models.Competitions;
using Newtonsoft.Json.Linq;

namespace Emando.Vantage.Api.Client.Competitions
{
    public class CompetitionsApiClient : ApiClient
    {
        public CompetitionsApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public CompetitionsApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public Task<CompetitionViewModel[]> GetCompetitionsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionViewModel[]>("competitions", cancellationToken);
        }

        public Task<CompetitionViewModel[]> GetCompetitionsAsync(bool editable = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            string relativeUri = editable ? "competitions/editable" : "competitions";
            return GetAsAsync<CompetitionViewModel[]>(relativeUri, cancellationToken);
        }

        public Task<CompetitionViewModel[]> GetCompetitionsAsync(string licenseIssuerId, string discipline, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionViewModel[]>($"competitions/{licenseIssuerId}/{discipline}", cancellationToken);
        }

        public Task<CompetitionViewModel[]> GetCompetitionsByLicenseAsync(string issuerId, string discipline, string key,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionViewModel[]>($"people/licenses/{issuerId}/{discipline}/competitions/{key}", cancellationToken);
        }

        public Task<CompetitionViewModel> GetCompetitionAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<CompetitionViewModel>($"competitions/{competitionId}", cancellationToken);
        }

        public Task<CompetitionViewModel> AddCompetitionAsync(CompetitionCreateModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostAsync<CompetitionCreateModel, CompetitionViewModel>("competitions", model, cancellationToken);
        }

        public Task UpdateCompetitionAsync(Guid competitionId, CompetitionUpdateModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PutAsync($"competitions/{competitionId}", model, cancellationToken);
        }

        public Task DeleteCompetitionAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return DeleteAsync($"competitions/{competitionId}", cancellationToken);
        }

        public Task<PersonLicensePriceViewModel> GetCompetitionLicenseRenewalPriceAsync(Guid competitionId, string key,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PersonLicensePriceViewModel>($"competitions/{competitionId}/licenses/prices/renew/{key}", cancellationToken);
        }

        public Task<PersonLicensePriceViewModel> GetCompetitionTemporaryLicensePriceAsync(Guid competitionId, Gender gender, DateTime birthDate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PersonLicensePriceViewModel>($"competitions/{competitionId}/licenses/prices/new/temporary/{gender}/{birthDate.ToString("yyyy-MM-dd")}",
                cancellationToken);
        }

        public Task<DistanceViewModel[]> GetDistancesAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<DistanceViewModel[]>($"competitions/{competitionId}/distances", cancellationToken);
        }

        public Task<DistanceCombinationViewModel[]> GetDistanceCombinationsAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<DistanceCombinationViewModel[]>($"competitions/{competitionId}/distancecombinations", cancellationToken);
        }

        public Task<PersonCompetitorListViewModel[]> GetPersonCompetitorListsAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PersonCompetitorListViewModel[]>($"competitions/{competitionId}/competitors/personlists", cancellationToken);
        }

        public Task<PersonCompetitorListViewModel> AddCompetitorListAsync(Guid competitionId, PersonCompetitorListCreateModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostAsync<PersonCompetitorListCreateModel, PersonCompetitorListViewModel>($"competitions/{competitionId}/competitors/personlists", model, cancellationToken);
        }

        public Task<PersonCompetitorDetailsViewModel[]> GetPersonCompetitorsAsync(Guid competitionId, Guid listId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<PersonCompetitorDetailsViewModel[]>($"competitions/{competitionId}/competitors/personlist/{listId}", cancellationToken);
        }

        public Task<PersonCompetitorDetailsViewModel> AddPersonCompetitorAsync(Guid competitionId, Guid listId, PersonCompetitorCreateModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostAsync<PersonCompetitorCreateModel, PersonCompetitorDetailsViewModel>($"competitions/{competitionId}/competitors/personlist/{listId}", model,
                cancellationToken);
        }

        public async Task<CompetitorListDetailsViewModel[]> GetCompetitorsAsync(Guid competitionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = await InvokeAsync($"competitions/{competitionId}/competitors", u => Client.GetAsync(u, cancellationToken));
            var json = await content.ReadAsStringAsync();

            var lists = new List<CompetitorListDetailsViewModel>();
            var token = JToken.Parse(json);
            foreach (var list in token)
                switch (list["typeName"].Value<string>())
                {
                    case "PersonCompetitorList":
                        lists.Add(list.ToObject<PersonCompetitorListDetailsViewModel>());
                        break;
                    case "TeamCompetitorList":
                        lists.Add(list.ToObject<TeamCompetitorListDetailsViewModel>());
                        break;
                }

            return lists.ToArray();
        }

        public Task<DistanceCombinationCompetitorsViewModel[]> GetDistanceCombinationsCompetitorsAsync(Guid competitionId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<DistanceCombinationCompetitorsViewModel[]>($"competitions/{competitionId}/distancecombinations/competitors", cancellationToken);
        }

        public Task<RaceViewModel[]> GetRacesAsync(Guid competitionId, Guid distanceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<RaceViewModel[]>($"competitions/{competitionId}/distance/{distanceId}/races", cancellationToken);
        }

        public Task<RacePassingViewModel[]> GetRacePassingsAsync(Guid competitionId, Guid raceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<RacePassingViewModel[]>($"competitions/{competitionId}/race/{raceId}/passings", cancellationToken);
        }
    }
}