namespace Emando.Vantage.Models
{
    public class VenueTrackViewModel : IVenueTrack
    {
        public string VenueCode { get; set; }

        public string VenueDiscipline { get; set; }

        public decimal Length { get; set; }
    }
}