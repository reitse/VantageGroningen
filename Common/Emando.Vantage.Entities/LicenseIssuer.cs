using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class LicenseIssuer
    {
        [Key]
        [StringLength(50)]
        [DataMember]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string Name { get; set; }

        [StringLength(200)]
        [DataMember]
        public string ForwardUri { get; set; }

        [StringLength(200)]
        [DataMember]
        public string EventUri { get; set; }

        [StringLength(20)]
        [DataMember]
        public string TemporaryKeyPrefix { get; set; }

        [DataMember]
        [Range(0, int.MaxValue)]
        public int TemporaryKeyFrom { get; set; }

        [StringLength(20)]
        [DataMember]
        public string DisposableKeyPrefix { get; set; }

        [DataMember]
        [Range(0, int.MaxValue)]
        public int DisposableKeyFrom { get; set; }
    }
}