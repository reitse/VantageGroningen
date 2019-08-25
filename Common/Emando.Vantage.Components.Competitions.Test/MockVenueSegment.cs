using Emando.Vantage.Entities;

namespace Emando.Vantage.Components.Competitions.Test
{
    public class MockVenueSegment : IVenueSegment
    {
        public MockVenueSegment(long @from, long to, int lane, decimal length, VenueSegmentFlags flags = VenueSegmentFlags.None)
        {
            From = @from;
            To = to;
            Lane = lane;
            Length = length;
            Flags = flags;
        }

        public long From { get; }

        public long To { get; }

        public int Lane { get; }

        public decimal Length { get; }

        public VenueSegmentFlags Flags { get; }
    }
}