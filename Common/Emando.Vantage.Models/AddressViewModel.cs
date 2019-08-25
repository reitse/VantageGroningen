using System.Globalization;

namespace Emando.Vantage.Models
{
    public class AddressViewModel : IAddress
    {
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string StateOrProvince { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string CountryCode { get; set; }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(CultureInfo cultureInfo)
        {
            switch (cultureInfo.Name)
            {
                case "nl-NL":
                    return $"{Line1}\r\n{PostalCode} {City}".Trim();

                default:
                    return $"{Line1}\r\n{Line2}\r\n{StateOrProvince} {PostalCode} {City}".Trim();
            }
        }
    }
}