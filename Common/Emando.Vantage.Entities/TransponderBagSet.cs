using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class TransponderBagSet
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string LicenseIssuerId { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        public string Discipline { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(20)]
        public string BagName { get; set; }

        public virtual TransponderBag Bag { get; set; }

        [Key, Column(Order = 3)]
        public int SetNumber { get; set; }

        public virtual TransponderSet Set { get; set; }

        public DateTime Added { get; set; }
    }
}