using System;

namespace Emando.Vantage
{
    public interface IPersonLicense
    {
        string IssuerId { get; }

        string Discipline { get; }

        string Key { get; }

        Guid PersonId { get; }

        string VenueCode { get; }

        string Sponsor { get; }

        string ClubCountryCode { get; }

        int? ClubCode { get; }

        PersonLicenseFlags Flags { get; }

        int Season { get; }

        DateTime ValidFrom { get; }

        DateTime ValidTo { get; }

        string Category { get; }

        int? Number { get; }

        string LegNumber { get; }

        string Transponder1 { get; }

        string Transponder2 { get; }
    }
}