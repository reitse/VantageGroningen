namespace Emando.Vantage.Models
{
    public class VenueDetailsViewModel : VenueViewModel
    {
        public VenueDistrictViewModel[] Districts { get; set; }

        public VenueTrackViewModel[] Tracks { get; set; }
    }
}