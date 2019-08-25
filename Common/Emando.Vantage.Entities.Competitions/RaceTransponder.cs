using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class RaceTransponder : IHaveTransponderKey
    {
        [Key, Column(Order = 0), DataMember]
        public Guid RaceId { get; set; }

        public virtual Race Race { get; set; }

        [DataMember]
        public virtual Transponder Transponder { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }

        [Key, ForeignKey("Transponder"), Column(Order = 1)]
        [DataMember]
        public string Type { get; set; }

        [Key, ForeignKey("Transponder"), Column(Order = 2)]
        [DataMember]
        public long Code { get; set; }

        [DataMember]
        public int? Set { get; set; }

        #region IHaveTransponderKey Members

        public TransponderKey TransponderKey => new TransponderKey(Type, Code);

        #endregion
    }
}