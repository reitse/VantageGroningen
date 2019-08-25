using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class PersonLicenseVenueClassBindingModel
    {
        [Required]
        public string Key { get; set; }

        public int Class { get; set; }
    }
}