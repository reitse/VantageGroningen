using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emando.Vantage.Entities
{
    public class ReportLogo
    {
        [Required]
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string LicenseIssuerId { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        [StringLength(100)]
        public string TemplateName { get; set; }

        public virtual ReportTemplate Template { get; set; }

        [Required]
        [StringLength(100)]
        [Key, Column(Order = 2)]
        public string Name { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}