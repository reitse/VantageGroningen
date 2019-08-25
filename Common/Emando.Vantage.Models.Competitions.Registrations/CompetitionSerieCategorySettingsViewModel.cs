using Emando.Vantage.Competitions.Registrations;

namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class CompetitionSerieCategorySettingsViewModel
    {
        public string Category { get; set; }

        public bool IsClosed { get; set; }

        public decimal? SeriePrice { get; set; }

        public decimal? CompetitionPrice { get; set; }

        public AllowedRegistrations AllowedRegistrations { get; set; }
    }
}