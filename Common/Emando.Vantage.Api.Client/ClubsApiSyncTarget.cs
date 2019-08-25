using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Logging;
using Emando.Vantage.Api.Models;
using Emando.Vantage.Components.Sync;

namespace Emando.Vantage.Api.Client
{
    public class ClubsApiSyncTarget : ISyncTarget<IClub>
    {
        private readonly ClubsApiClient client;
        private readonly ISyncSource<IClub> source;
        private readonly ILog log;

        public ClubsApiSyncTarget(ClubsApiClient client)
        {
            this.client = client;

            log = LogManager.GetLogger(GetType());
            source = new ClubsApiSyncSource(client);
        }

        #region ISyncTarget<IClub> Members

        public string Source => source.Source;

        public IEnumerable<IClub> Extract(CancellationToken cancellationToken = new CancellationToken())
        {
            return source.Extract(cancellationToken);
        }

        public string Target => client.BaseUri.ToString();

        public bool CanDelete(IClub club)
        {
            return false;
        }

        public Task DeleteAsync(IEnumerable<IClub> items, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotSupportedException();
        }

        public bool CanUpdate(IClub club)
        {
            return true;
        }

        public async Task UpdateAsync(IEnumerable<IClub> items, CancellationToken cancellationToken)
        {
            await Task.WhenAll(items.Select(async c =>
            {
                try
                {
                    await client.UpdateClubAsync(c.CountryCode, c.Code, Mapper.Map<ClubUpdateModel>(c), cancellationToken);
                }
                catch (Exception e)
                {
                    log.Warn(l => l($"Failed to update club {c}: {e.Message}"), e);
                }
            }));
        }

        public bool CanInsert(IClub club)
        {
            return true;
        }

        public async Task InsertAsync(IEnumerable<IClub> items, CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.WhenAll(items.Select(async c =>
            {
                try
                {
                    await client.AddClubAsync(Mapper.Map<ClubCreateModel>(c), cancellationToken);
                }
                catch (Exception e)
                {
                    log.Warn(l => l($"Failed to add club {c}: {e.Message}"), e);
                }
            }));
        }

        #endregion
    }
}