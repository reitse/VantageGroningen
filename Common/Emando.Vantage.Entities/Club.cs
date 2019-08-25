using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class Club : IClub
    {
        public virtual Country Country { get; set; }

        public virtual ICollection<ClubVenue> Venues { get; set; }

        #region IClub Members

        [Key, Column(Order = 0)]
        [Index]
        [DataMember]
        public string CountryCode { get; set; }

        [Key, Column(Order = 1)]
        [Range(0, int.MaxValue)]
        [DataMember]
        public int Code { get; set; }

        [StringLength(20)]
        [DataMember]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string FullName { get; set; }

        #endregion
    }
}