using System.Collections.Generic;

namespace Emando.Vantage
{
    public interface IVenue
    {
         string Code { get; }

         string Discipline { get; }

         string Name { get; }

         IAddress Address { get; }

         string ContinentCode { get; }

         IEnumerable<IVenueTrack> Tracks { get; }
    }
}
