using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class PersonCategory : IPersonCategory
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string LicenseIssuerId { get; set; }

        public virtual LicenseIssuer LicenseIssuer { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        public string Discipline { get; set; }

        [Range(0, int.MaxValue)]
        public int FromAge { get; set; }

        [Range(0, int.MaxValue)]
        public int ToAge { get; set; }

        public Gender Gender { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(20)]
        [Required]
        public string Code { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public PersonCategoryFlags Flags { get; set; }
    }
}