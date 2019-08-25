using System;

namespace Emando.Vantage.Competitions
{
    public interface ICompetition
    {
        Guid Id { get; }

        Guid? SerieId { get; }

        string ProviderKey { get; }

        string LicenseIssuerId { get; }

        string Discipline { get; }

        string VenueCode { get; }

        string Sponsor { get; }

        string Name { get; }

        int Class { get; }

        string Culture { get; }

        string TimeZone { get; }

        string ReportTemplateName { get; }

        DateTime Starts { get; }

        DateTime? Ends { get; }

        string Location { get; }

        CompetitionLocationFlags LocationFlags { get; }

        string Extra { get; }
    }
}