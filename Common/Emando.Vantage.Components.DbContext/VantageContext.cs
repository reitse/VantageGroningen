using System.Data.Common;
using System.Data.Entity;
using Emando.Vantage.Components.Migrations;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components
{
    public class VantageContext : DataContext, IVantageContext
    {
        public VantageContext()
        {
        }

        public VantageContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VantageContext, Configuration>(true));
        }

        public VantageContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VantageContext, Configuration>(true));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<Name>();
            modelBuilder.ComplexType<Contact>();
            modelBuilder.ComplexType<Address>();

            modelBuilder.Entity<PersonLicense>().HasOptional(l => l.Venue).WithMany().HasForeignKey(l => new
            {
                l.VenueCode,
                l.Discipline
            });
            modelBuilder.Entity<PersonCategory>().HasRequired(c => c.LicenseIssuer).WithMany().HasForeignKey(c => c.LicenseIssuerId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Venue>().HasMany(v => v.Districts).WithMany(d => d.Venues)
                .Map(c => c.MapLeftKey("VenueCode", "VenueDiscipline").MapRightKey("DistrictLevel", "DistrictCode").ToTable("VenueVenueDistricts"));

            modelBuilder.Entity<ReportLogo>().ToTable("ReportLogos");
            modelBuilder.Entity<ReportLogo>().HasRequired(l => l.Template).WithMany(l => l.Logos).HasForeignKey(l => new
            {
                l.LicenseIssuerId,
                l.TemplateName
            }).WillCascadeOnDelete(true);

            modelBuilder.Entity<VenueTrack>().Property(e => e.Length).HasPrecision(18, 3);
            modelBuilder.Entity<Person>().Property(p => p.BirthDate).HasColumnType("date");
            modelBuilder.Entity<PersonLicense>().Property(p => p.ValidFrom).HasColumnType("date");
            modelBuilder.Entity<PersonLicense>().Property(p => p.ValidTo).HasColumnType("date");
            modelBuilder.Entity<PersonLicenseVenueSubscription>().Property(p => p.ValidFrom).HasColumnType("date");
            modelBuilder.Entity<PersonLicenseVenueSubscription>().Property(p => p.ValidTo).HasColumnType("date");

            modelBuilder.Entity<TransponderBagSet>().HasRequired(s => s.Bag).WithMany(b => b.Sets).HasForeignKey(s => new
            {
                s.LicenseIssuerId,
                s.Discipline,
                s.BagName
            }).WillCascadeOnDelete(true);
            modelBuilder.Entity<TransponderBagSet>().HasRequired(s => s.Set).WithMany().HasForeignKey(s => new
            {
                s.LicenseIssuerId,
                s.Discipline,
                s.SetNumber
            }).WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        #region IVantageContext Members

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Continent> Continents { get; set; }

        public IDbSet<LicenseIssuer> LicenseIssuers { get; set; }

        public IDbSet<Club> Clubs { get; set; } 

        public IDbSet<ClubVenue> ClubVenues { get; set; }

        public IDbSet<Person> Persons { get; set; }

        public IDbSet<PersonLicense> PersonLicenses { get; set; }

        public IDbSet<PersonLicenseVenueSubscription>  PersonLicenseVenueSubscriptions { get; set; }

        public IDbSet<PersonLicenseVenueClass> PersonLicenseVenueClasses { get; set; }

        public IDbSet<PersonCategory> PersonCategories { get; set; }

        public IDbSet<Venue> Venues { get; set; }

        public IDbSet<VenueTrack> VenueTracks { get; set; }

        public IDbSet<VenueDistrict> VenueDistricts { get; set; }

        public IDbSet<Transponder> Transponders { get; set; }

        public IDbSet<TransponderSet> TransponderSets { get; set; } 

        public IDbSet<TransponderBag> TransponderBags { get; set; }

        public IDbSet<TransponderBagSet> TransponderBagSets { get; set; }

        public IDbSet<TransponderSetTransponder> TransponderSetTransponders { get; set; }

        public IDbSet<PersonLicensePrice> PersonLicensePrices { get; set; }

        public IDbSet<ReportTemplate> ReportTemplates { get; set; }

        public IDbSet<ReportLogo> ReportLogos { get; set; }

        #endregion
    }
}