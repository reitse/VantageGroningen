using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Workflows
{
    public class TranspondersWorkflow : IDisposable
    {
        private readonly IVantageContext context;
        private readonly ITransponderCodeConverter transponderCodeConverter;
        private bool isDisposed;

        public TranspondersWorkflow(IVantageContext context, ITransponderCodeConverter transponderCodeConverter)
        {
            this.context = context;
            this.transponderCodeConverter = transponderCodeConverter;

            context.ProxyCreationEnabled = false;
            context.LazyLoadingEnabled = false;
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

        ~TranspondersWorkflow()
        {
            Dispose(false);
        }

        #region Transponders

        public IQueryable<Transponder> Transponders => context.Transponders;

        public IQueryable<TransponderSet> TransponderSets => context.TransponderSets; 

        public bool TryConvertLabel(string type, string label, out long code)
        {
            return transponderCodeConverter.TryConvertLabel(type, label, out code);
        }

        public async Task AddTransponderAsync(Transponder transponder)
        {
            context.Transponders.Add(transponder);
            await context.SaveChangesAsync();
        }

        #endregion

        #region Bags

        public IQueryable<TransponderBag> Bags(string licenseIssuerId, string discipline)
        {
            return from b in context.TransponderBags
                   where b.LicenseIssuerId == licenseIssuerId && b.Discipline == discipline
                   select b;
        }

        public async Task AddBagAsync(TransponderBag bag)
        {
            context.TransponderBags.Add(bag);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBagAsync(TransponderBag bag)
        {
            context.TransponderBags.Remove(bag);
            await context.SaveChangesAsync();
        }

        #endregion

        #region Bags

        public IQueryable<TransponderSet> BagTransponders(string licenseIssuerId, string discipline, string name)
        {
            return from s in context.TransponderBagSets.Include(s => s.Set.Transponders.Select(t => t.Transponder))
                   where s.LicenseIssuerId == licenseIssuerId && s.Discipline == discipline && s.BagName == name
                   orderby s.SetNumber
                   select s.Set;
        }

        public async Task<ICollection<TransponderBagSet>> ResetBagAsync(string licenseIssuerId, string discipline, string name, IEnumerable<int> sets)
        {
            var now = DateTime.UtcNow;
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var bag = await context.TransponderBags.Include(b => b.Sets).FirstOrDefaultAsync(
                        b => b.LicenseIssuerId == licenseIssuerId && b.Discipline == discipline && b.Name == name);
                    if (bag == null)
                        throw new TransponderBagNotFoundException();

                    foreach (var set in bag.Sets.ToList())
                        context.TransponderBagSets.Remove(set);
                    await context.SaveChangesAsync();

                    var bagSets = new List<TransponderBagSet>();
                    foreach (var setNumber in sets)
                    {
                        var set = await context.TransponderSets.FirstOrDefaultAsync(
                            s => s.LicenseIssuerId == licenseIssuerId && s.Discipline == discipline && s.Number == setNumber);

                        var bagSet = new TransponderBagSet
                        {
                            Bag = bag,
                            Set = set,
                            Added = now
                        };
                        context.TransponderBagSets.Add(bagSet);
                        bagSets.Add(bagSet);
                        await context.SaveChangesAsync();
                    }

                    transaction.Commit();
                    return bagSets;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        #endregion
    }
}