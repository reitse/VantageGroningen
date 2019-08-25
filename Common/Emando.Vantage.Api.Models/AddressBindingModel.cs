using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class AddressBindingModel
    {
        [StringLength(100)]
        public string Line1 { get; set; }
        [StringLength(100)]
        public string Line2 { get; set; }
        [StringLength(50)]
        public string StateOrProvince { get; set; }
        [StringLength(20)]
        public string PostalCode { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(3, MinimumLength = 3)]
        public string CountryCode { get; set; }

        public void SetDefaultCasing()
        {
            if (Line1 != null)
                Line1 = Line1.ToInvariantTitleCase();
            if (Line2 != null)
                Line2 = Line2.ToInvariantTitleCase();
            if (StateOrProvince != null)
                StateOrProvince = StateOrProvince.ToInvariantTitleCase();
            if (PostalCode != null)
                PostalCode = PostalCode.ToUpper();
            if (City != null)
                City = City.ToInvariantTitleCase();
            if (CountryCode != null)
                CountryCode = CountryCode.ToUpper();
        }
    }
}