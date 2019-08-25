using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Emando.Vantage.Entities.Identity
{
    public class VantageUser : IdentityUser<string, IdentityUserLogin, VantageUserRole, IdentityUserClaim>, IVantageUser
    {
        public Name Name { get; set; }

        public DateTime? LastLogin { get; set; }

        public virtual ICollection<VantageUserCompetitionRight> CompetitionRights { get; set; }

        public bool IsLockedOut => LockoutEnabled && LockoutEndDateUtc > DateTime.UtcNow;

        [StringLength(128)]
        public string OwnerId { get; set; }
    }
}