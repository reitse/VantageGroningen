using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class Venue : IVenue
    {
        public Venue()
        {
            Address = new Address();
        }

        [Key, Column(Order = 0)]
        [StringLength(50)]
        [DataMember]
        public string Code { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        [DataMember]
        public string Discipline { get; set; }

        [Required, StringLength(100)]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public string ContinentCode { get; set; }

        public virtual Continent Continent { get; set; }

        public virtual ICollection<VenueTrack> Tracks { get; set; }

        public virtual ICollection<VenueDistrict> Districts { get; set; }

        IAddress IVenue.Address => Address;

        IEnumerable<IVenueTrack> IVenue.Tracks => Tracks;

        public override string ToString()
        {
            return $"{Address.City} ({Name})";
        }
    }
}