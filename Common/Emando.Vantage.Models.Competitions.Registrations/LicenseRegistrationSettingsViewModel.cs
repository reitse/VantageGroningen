namespace Emando.Vantage.Models.Competitions.Registrations
{
    public class LicenseRegistrationSettingsViewModel
    {
        public DistanceCombinationRegistrationSettingsViewModel[] DistanceCombinations { get; set; }

        public bool IsRegisteredForSerie { get; set; }

        public bool IsRegisteredForCompetition { get; set; }
    }
}