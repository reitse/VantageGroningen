using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class TransponderSetTransponder : IHaveTransponderKey
    {
        [Key, Column(Order = 0)]
        public string SetLicenseIssuerId { get; set; }

        [Key, Column(Order = 1)]
        public string SetDiscipline { get; set; }

        [Key, Column(Order = 2)]
        public int SetNumber { get; set; }

        public virtual TransponderSet Set { get; set; }

        [Key, Column(Order = 3)]
        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string TransponderType { get; set; }

        [DataMember]
        public long TransponderCode { get; set; }

        public virtual Transponder Transponder { get; set; }

        #region IHaveTransponderKey Members

        public TransponderKey TransponderKey => new TransponderKey(TransponderType, TransponderCode);

        #endregion
    }
}