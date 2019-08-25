using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Logging;
using Emando.Vantage.Api.Models.Competitions;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Sync;

namespace Emando.Vantage.Api.Client.Competitions
{
    public class CompetitionsApiSyncTarget : ISyncTarget<ICompetition>
    {
        private readonly CompetitionsApiClient client;
        private readonly ILog log;
        private readonly ISyncSource<ICompetition> source;

        public CompetitionsApiSyncTarget(CompetitionsApiClient client, string licenseIssuerId = null, bool requireProviderKey = false)
        {
            this.client = client;

            log = LogManager.GetLogger(GetType());
            source = new CompetitionsApiSyncSource(client, licenseIssuerId, requireProviderKey);
        }

        #region ISyncTarget<ICompetition> Members

        public string Target => client.BaseUri.ToString();

        public bool CanDelete(ICompetition competition)
        {
            return true;
        }

        public Task DeleteAsync(IEnumerable<ICompetition> items, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.WhenAll(items.Select(async c =>
            {
                try
                {
                    await client.DeleteCompetitionAsync(c.Id, cancellationToken);
                }
                catch (Exception e)
                {
                    log.Warn(l => l($"Failed to delete competition {c}: {e.Message}"));
                }
            }));
        }

        public bool CanUpdate(ICompetition competition)
        {
            return true;
        }

        public Task UpdateAsync(IEnumerable<ICompetition> items, CancellationToken cancellationToken)
        {
            return Task.WhenAll(items.Select(async c =>
            {
                try
                {
                    await client.UpdateCompetitionAsync(c.Id, Mapper.Map<CompetitionUpdateModel>(c), cancellationToken);
                }
                catch (Exception e)
                {
                    log.Warn(l => l($"Failed to update competition {c}: {e.Message}"));
                }
            }));
        }

        public bool CanInsert(ICompetition competition)
        {
            return true;
        }

        public Task InsertAsync(IEnumerable<ICompetition> items, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.WhenAll(items.Select(async c =>
            {
                try
                {
                    var model = Mapper.Map<CompetitionCreateModel>(c);
                    await client.AddCompetitionAsync(model, cancellationToken);
                }
                catch (Exception e)
                {
                    log.Warn(l => l($"Failed to add competition {c}: {e.Message}"));
                }
            }));
        }

        public string Source => source.Source;

        public IEnumerable<ICompetition> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            return source.Extract(cancellationToken);
        }

        #endregion
    }
}