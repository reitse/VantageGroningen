namespace Emando.Vantage
{
    public interface IClub
    {
        string CountryCode { get; }

        int Code { get; }

        string ShortName { get; }

        string FullName { get; }
    }
}