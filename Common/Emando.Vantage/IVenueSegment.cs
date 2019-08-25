namespace Emando.Vantage
{
    public interface IVenueSegment
    {
        long From { get; }

        long To { get; }

        int Lane { get; }

        decimal Length { get; }

        VenueSegmentFlags Flags { get; }
    }
}