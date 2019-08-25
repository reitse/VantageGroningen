using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emando.Vantage.Entities
{
    public class PersonLicensePrice
    {
        [Key, Column(Order = 0)]
        public string LicenseIssuerId { get; set; }

        public virtual LicenseIssuer LicenseIssuer { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        public string Discipline { get; set; }

        [Key, Column(Order = 2)]
        [Range(0, int.MaxValue)]
        public int FromAge { get; set; }

        [Range(0, int.MaxValue)]
        public int ToAge { get; set; }

        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; }

        public decimal Price { get; set; }

        public PersonLicenseFlags Flags { get; set; }
    }
}