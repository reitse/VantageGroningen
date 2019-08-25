using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2015/06/Entities")]
    public class Continent
    {
        [Key, StringLength(3, MinimumLength = 3)]
        [DataMember]
        public string Code { get; set; }
    }
}