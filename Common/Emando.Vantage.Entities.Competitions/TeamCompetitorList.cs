using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class TeamCompetitorList : CompetitorListBase
    {
        [DataMember]
        public Guid PersonsId { get; set; }

        public virtual PersonCompetitorList Persons { get; set; }
    }
}