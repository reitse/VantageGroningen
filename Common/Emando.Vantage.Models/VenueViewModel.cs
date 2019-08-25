using System.Linq;
using System.Collections.Generic;

namespace Emando.Vantage.Models
{
    public class VenueViewModel : IVenue
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Discipline { get; set; }

        public AddressViewModel Address { get; set; }

        public string ContinentCode { get; set; }

        public VenueTrackViewModel[] Tracks { get; set; }

        IAddress IVenue.Address => Address;

        IEnumerable<IVenueTrack> IVenue.Tracks => Tracks;
    }
}