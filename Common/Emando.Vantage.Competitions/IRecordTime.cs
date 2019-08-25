using System;

namespace Emando.Vantage.Competitions
{
    public interface IRecordTime
    {
        string LicenseIssuerId { get; }

        RecordType Type { get; }

        string Discipline { get; }

        Gender Gender { get; }

        int FromAge { get; }

        int ToAge { get; }

        string VenueCode { get; }

        string DistanceDiscipline { get; }

        int Distance { get; }

        DateTime Date { get; }

        TimeSpan Time { get; }

        string Name { get; }

        string NationalityCode { get; }
    }
}