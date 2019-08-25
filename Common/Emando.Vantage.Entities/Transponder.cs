using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class Transponder : IHaveTransponderKey
    {
        [StringLength(50)]
        [DataMember]
        public string Label { get; set; }

        [Key, StringLength(100), Column(Order = 0)]
        [DataMember]
        public string Type { get; set; }

        [Key, Column(Order = 1)]
        [DataMember]
        public long Code { get; set; }

        public virtual ICollection<TransponderSetTransponder> Sets { get; set; }

        #region IHaveTransponderKey Members

        public TransponderKey TransponderKey => new TransponderKey(Type, Code);

        #endregion
    }
}