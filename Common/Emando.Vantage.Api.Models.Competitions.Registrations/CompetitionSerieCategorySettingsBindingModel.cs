using Emando.Vantage.Competitions.Registrations;

namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class CompetitionSerieCategorySettingsBindingModel
    {
        public string Category { get; set; }

        public bool IsClosed { get; set; }

        public decimal? SeriePrice { get; set; }

        public decimal? CompetitionPrice { get; set; }

        public AllowedRegistrations AllowedRegistrations { get; set; }
    }
}