using System;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    [Flags]
    public enum OptionalReportColumns
    {
        None = 0x0,
        HomeVenueCode = 0x1,
        NationalityCode = 0x2,
        ClubShortName = 0x4,
        AreaCode = 0x8,
        LicenseKey = 0x10,
        SeasonBest = 0x20
    }
}