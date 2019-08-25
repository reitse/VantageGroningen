using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class RaceTime : IRaceTime
    {
        [Key, Column(Order = 0), DataMember]
        public Guid RaceId { get; set; }

        public virtual Race Race { get; set; }

        [StringLength(50), DataMember]
        public string ApplianceName { get; set; }

        [StringLength(50), DataMember]
        public string ApplianceInstanceName { get; set; }

        [Key, Column(Order = 1), Required, StringLength(50), DataMember]
        public string InstanceName { get; set; }

        [StringLength(50), DataMember]
        public string How { get; set; }

        [DataMember]
        public TimeInfo TimeInfo { get; set; }

        public PresentationSource PresentationSource
        {
            get { return new PresentationSource(ApplianceName, ApplianceInstanceName, How); }
            set
            {
                ApplianceName = value.ApplianceName;
                ApplianceInstanceName = value.ApplianceInstanceName;
                How = value.How;
            }
        }

        #region IRaceTime Members

        [DataMember]
        public TimeSpan Time { get; set; }

        #endregion
    }
}