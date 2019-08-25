using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emando.Vantage.Server.Entities.Appliances.MylapsOrbits
{
    public class OrbitsInstance
    {
        [Key, Column(Order = 0)]
        [Required]
        public string VenueCode { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        [Required]
        public string Discipline { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Host { get; set; }

        public int Port { get; set; }
    }
}