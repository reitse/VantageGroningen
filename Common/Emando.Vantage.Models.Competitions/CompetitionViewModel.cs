using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class CompetitionViewModel : ICompetition
    {
        public CompetitionSerieViewModel Serie { get; set; }

        public VenueViewModel Venue { get; set; }

        public CompetitionResultsStatus ResultsStatus { get; set; }

        public DateTime? MadeOfficial { get; set; }

        #region ICompetition Members

        Guid? ICompetition.SerieId => Serie?.Id;

        public string Location { get; set; }

        public CompetitionLocationFlags LocationFlags { get; set; }

        public string Extra { get; set; }

        public Guid Id { get; set; }

        public string ProviderKey { get; set; }

        public string Discipline { get; set; }

        string ICompetition.VenueCode => Venue?.Code;

        public string Sponsor { get; set; }

        public string Name { get; set; }

        public int Class { get; set; }

        public string Culture { get; set; }

        public string TimeZone { get; set; }

        public string LicenseIssuerId { get; set; }

        public string ReportTemplateName { get; set; }

        public DateTime Starts { get; set; }

        public DateTime? Ends { get; set; }

        #endregion
    }
}