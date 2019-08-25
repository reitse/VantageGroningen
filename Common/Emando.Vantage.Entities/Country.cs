using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class Country
    {
        [Key, StringLength(3, MinimumLength = 3)]
        [DataMember]
        public string Code { get; set; }

        [Required, StringLength(100)]
        [DataMember]
        public string Name { get; set; }
    }
}