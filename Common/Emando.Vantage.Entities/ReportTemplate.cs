using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    public class ReportTemplate
    {
        [Required]
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string LicenseIssuerId { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        [StringLength(100)]
        public string Name { get; set; }

        [DataMember]
        public virtual ICollection<ReportLogo> Logos { get; set; }
    }
}