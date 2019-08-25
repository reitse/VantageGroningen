using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Identity
{
    public class ClientApplicationCompetitionRight : ICompetitionRight
    {
        [Key, Column(Order = 0)]
        public string ClientApplicationId { get; set; }

        public virtual ClientApplication ClientApplication { get; set; }

        [Key, Column(Order = 5)]
        public string RoleId { get; set; }

        #region ICompetitionRight Members

        [Key, Column(Order = 1)]
        public string LicenseIssuerId { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(100)]
        public string Discipline { get; set; }

        [Key, Column(Order = 3)]
        [Range(0, int.MaxValue)]
        public int CompetitionClass { get; set; }

        [Key, Column(Order = 4)]
        [StringLength(100)]
        public string Value { get; set; }

        string ICompetitionRight.RoleName => Role.Name;

        public virtual VantageRole Role { get; set; }

        #endregion
    }
}