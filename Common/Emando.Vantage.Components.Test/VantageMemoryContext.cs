using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components.Test
{
    public class VantageMemoryContext : IVantageContext
    {
        public IDbSet<VenueDistrict> VenueLicenseAreas { get; } = new MemoryDbSet<VenueDistrict>();

        #region IVantageContext Members

        public IDbSet<VenueDistrict> VenueDistricts { get; } = new MemoryDbSet<VenueDistrict>();

        public IDbSet<Continent> Continents { get; } = new MemoryDbSet<Continent>();

        public IDbSet<Venue> Venues { get; } = new MemoryDbSet<Venue>();

        public IDbSet<VenueTrack> VenueTracks { get; } = new MemoryDbSet<VenueTrack>();

        public IDbSet<Club> Clubs { get; } = new MemoryDbSet<Club>();

        public IDbSet<PersonCategory> PersonCategories { get; } = new MemoryDbSet<PersonCategory>();

        public IDbSet<Transponder> Transponders { get; } = new MemoryDbSet<Transponder>();

        public IDbSet<TransponderBag> TransponderBags { get; } = new MemoryDbSet<TransponderBag>();

        public IDbSet<TransponderBagSet> TransponderBagSets { get; } = new MemoryDbSet<TransponderBagSet>();

        public IDbSet<TransponderSetTransponder> TransponderSetTransponders { get; } = new MemoryDbSet<TransponderSetTransponder>();

        public IDbSet<TransponderSet> TransponderSets { get; } = new MemoryDbSet<TransponderSet>();

        public IDbSet<PersonLicensePrice> PersonLicensePrices { get; } = new MemoryDbSet<PersonLicensePrice>();

        public IDbSet<ReportTemplate> ReportTemplates { get; } = new MemoryDbSet<ReportTemplate>();

        public IDbSet<ReportLogo> ReportLogos { get; } = new MemoryDbSet<ReportLogo>();

        public IDbSet<LicenseIssuer> LicenseIssuers { get; } = new MemoryDbSet<LicenseIssuer>();

        public IDbSet<Person> Persons { get; } = new MemoryDbSet<Person>();

        public IDbSet<PersonLicense> PersonLicenses { get; } = new MemoryDbSet<PersonLicense>();

        public IDbSet<PersonLicenseVenueSubscription> PersonLicenseVenueSubscriptions { get; } = new MemoryDbSet<PersonLicenseVenueSubscription>();

        public IDbSet<PersonLicenseVenueClass> PersonLicenseVenueClasses { get; } = new MemoryDbSet<PersonLicenseVenueClass>();

        public IDbSet<Country> Countries { get; } = new MemoryDbSet<Country>();

        public bool LazyLoadingEnabled { get; set; }

        public bool ProxyCreationEnabled { get; set; }

        public bool AutoDetectChangesEnabled { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public IContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new MockContextTransaction();
        }

        public IContextTransaction UseOrBeginTransaction(IsolationLevel isolationLevel)
        {
            return new MockContextTransaction();
        }

        public Task LoadAsync<TEntity, TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty) where TEntity : class
            where TElement : class
        {
            return Task.FromResult<object>(null);
        }

        public Task LoadAsync<TEntity, TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty,
            Expression<Func<TElement, bool>> predicate) where TEntity : class where TElement : class
        {
            return Task.FromResult<object>(null);
        }

        public Task LoadAsync<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty) where TEntity : class where TProperty : class
        {
            return Task.FromResult<object>(null);
        }

        public void Initialize(bool force)
        {
        }

        public void Dispose()
        {
        }

        #endregion

        public void SetModified<T>(T entity) where T : class
        {
        }
    }
}