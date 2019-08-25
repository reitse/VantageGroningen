namespace Emando.Vantage
{
    public interface IVenueTrack
    {
        string VenueCode { get; }

        string VenueDiscipline { get; }

        decimal Length { get; }
    }
}
