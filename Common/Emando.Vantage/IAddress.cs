namespace Emando.Vantage
{
    public interface IAddress
    {
        string City { get; }

        string CountryCode { get; }

        string Line1 { get; }

        string Line2 { get; }

        string PostalCode { get; }

        string StateOrProvince { get; }
    }
}