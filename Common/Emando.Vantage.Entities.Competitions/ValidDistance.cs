using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class ValidDistance
    {
        [Key, Column(Order = 0)]
        [StringLength(100)]
        [DataMember]
        public string Discipline { get; set; }

        [Key, Column(Order = 1)]
        [DataMember]
        public int Value { get; set; }
    }
}