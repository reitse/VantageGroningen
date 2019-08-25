using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Identity
{
    public class VantageUserCompetitionRight : ICompetitionRight
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        public virtual VantageUser User { get; set; }

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

        [Key, Column(Order = 5)]
        public string RoleId { get; set; }

        string ICompetitionRight.RoleName => Role.Name;

        public virtual VantageRole Role { get; set; }

        public static VantageUserCompetitionRight Decode(string userId, string value)
        {
            var parts = value.Split('/');
            var classValue = parts[2].Split(':');
            return new VantageUserCompetitionRight
            {
                UserId = userId,
                LicenseIssuerId = parts[0],
                Discipline = parts[1],
                CompetitionClass = int.Parse(classValue[0]),
                Value = classValue[1],
                Role = new VantageRole(parts[3])
            };
        }
    }
}