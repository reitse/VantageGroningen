using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class DistancePointsTablesWorkflow : IDisposable
    {
        private readonly ICompetitionContext context;
        private bool isDisposed;

        public DistancePointsTablesWorkflow(ICompetitionContext context)
        {
            this.context = context;
        }

        public IQueryable<DistancePointsTable> DistancePointsTables => context.DistancePointsTables.Include(t => t.Points);

        public async Task AddAsync(DistancePointsTable table)
        {
            table.Id = Guid.NewGuid();
            context.DistancePointsTables.Add(table);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(DistancePointsTable table)
        {
            context.DistancePointsTables.Remove(table);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DistancePointsTable table, params DistancePoints[] points)
        {
            using (var transaction = context.BeginTransaction(IsolationLevel.Serializable))
                try
                {
                    foreach (var p in table.Points.ToList())
                        context.DistancePoints.Remove(p);
                    await context.SaveChangesAsync();

                    foreach (var p in points)
                        table.Points.Add(p);
                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    context.Dispose();

                isDisposed = true;
            }
        }
    }
}