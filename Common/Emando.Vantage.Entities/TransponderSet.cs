using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emando.Vantage.Entities
{
    public class TransponderSet
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string LicenseIssuerId { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        public string Discipline { get; set; }

        [Key, Column(Order = 2)]
        public int Number { get; set; }

        public ICollection<TransponderSetTransponder> Transponders { get; set; }
    }
}