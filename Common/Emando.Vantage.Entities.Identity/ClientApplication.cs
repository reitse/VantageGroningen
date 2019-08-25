using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Entities.Identity
{
    public class ClientApplication
    {
        [Key]
        public string Id { get; set; }
        
        [Required]
        public string Secret { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool KnowsSecret { get; set; }

        public bool IsActive { get; set; }

        public int CompetitionClass { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        [MaxLength(100)]
        public string AllowedOrigin { get; set; }

        public virtual ICollection<VantageRole> Roles { get; set; }

        public virtual ICollection<ClientApplicationCompetitionRight> CompetitionRights { get; set; }
    }
}