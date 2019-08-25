using System.Globalization;

namespace Emando.Vantage.Models
{
    public class RestrictedAddressViewModel
    {
        public string City { get; set; }

        public string CountryCode { get; set; }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(CultureInfo cultureInfo)
        {
            return City;
        }
    }
}