using System.Data.Entity;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components
{
    public interface IVantageContext : IDataContext
    {
        IDbSet<Venue> Venues { get; }

        IDbSet<VenueTrack> VenueTracks { get; }

        IDbSet<VenueDistrict> VenueDistricts { get; }

        IDbSet<Continent> Continents { get; }

        IDbSet<Country> Countries { get; }

        IDbSet<LicenseIssuer> LicenseIssuers { get; }

        IDbSet<Club> Clubs { get; }

        IDbSet<ClubVenue> ClubVenues { get; }

        IDbSet<Person> Persons { get; }

        IDbSet<PersonLicense> PersonLicenses { get; }

        IDbSet<PersonLicenseVenueSubscription> PersonLicenseVenueSubscriptions { get; }

        IDbSet<PersonLicenseVenueClass> PersonLicenseVenueClasses { get; }

        IDbSet<PersonCategory> PersonCategories { get; }

        IDbSet<Transponder> Transponders { get; }

        IDbSet<TransponderSet> TransponderSets { get; }

        IDbSet<TransponderBag> TransponderBags { get; }

        IDbSet<TransponderBagSet> TransponderBagSets { get; }

        IDbSet<TransponderSetTransponder> TransponderSetTransponders { get; }

        IDbSet<PersonLicensePrice> PersonLicensePrices { get; }

        IDbSet<ReportTemplate> ReportTemplates { get; } 

        IDbSet<ReportLogo> ReportLogos { get; }
    }
}