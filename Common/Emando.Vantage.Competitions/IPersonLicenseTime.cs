using System;

namespace Emando.Vantage.Competitions
{
    public interface IPersonLicenseTime
    {
        string LicenseIssuerId { get; }

        string LicenseDiscipline { get; }

        string LicenseKey { get; }

        string VenueCode { get; }

        string Discipline { get; }

        string DistanceDiscipline { get; }

        int Distance { get; }

        DateTime Date { get; }

        TimeSpan Time { get; }

        Guid? CompetitionId { get; }

        string NationalityCode { get; }

        string Source { get; }
    }
}