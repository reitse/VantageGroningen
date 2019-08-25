using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Emando.Vantage.Components.Sync.Properties;

namespace Emando.Vantage.Components.Sync
{
    public class SyncService<TEntity>
    {
        public static async Task SyncAsync<TSource, TTarget>(TSource source, TTarget target, IEqualityComparer<TEntity> comparer,
            CancellationToken cancellationToken = default(CancellationToken), bool insert = true, bool update = true, bool delete = true)
            where TSource : ISyncSource<TEntity>
            where TTarget : ISyncTarget<TEntity>
        {
            var log = LogManager.GetLogger<SyncService<TEntity>>();

            log.Info(l => l(Resources.FetchingItems, source.Source, source.GetType().Name, target.Target, target.GetType().Name));
            var sourceItems = Task.Run(() => source.Extract(cancellationToken).ToList(), cancellationToken);
            var targetItems = Task.Run(() => target.Extract(cancellationToken).ToList(), cancellationToken);
            await Task.WhenAll(sourceItems, targetItems);

            log.Info(l => l(Resources.ItemsFetched, sourceItems.Result.Count, targetItems.Result.Count));

            if (delete)
            {
                log.Info(l => l(Resources.DeletingItems));
                var deletes = targetItems.Result.Except(sourceItems.Result, comparer);
                await target.DeleteAsync(deletes, cancellationToken);
            }

            if (update)
            {
                log.Info(l => l(Resources.UpdatingItems));
                var updates = sourceItems.Result.Intersect(targetItems.Result, comparer);
                await target.UpdateAsync(updates, cancellationToken);
            }

            if (insert)
            {
                log.Info(l => l(Resources.InsertingItems));
                var inserts = sourceItems.Result.Except(targetItems.Result, comparer);
                await target.InsertAsync(inserts, cancellationToken);
            }
        }
    }
}