using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class TeamCompetitor : CompetitorBase
    {
        [Required, StringLength(100)]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual ICollection<TeamCompetitorMember> Members { get; set; }

        public override string FullName => Name;

        public override string ToString()
        {
            return FullName;
        }
    }
}