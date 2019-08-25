using System.Collections.Generic;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public interface IVenueDistance
    {
        IReadOnlyCollection<IVenueSegment> Segments { get; }

        IReadOnlyDictionary<int, IDistanceLaneLocations> Locations { get; }

        bool IsFinish(int lane, long where);
    }
}