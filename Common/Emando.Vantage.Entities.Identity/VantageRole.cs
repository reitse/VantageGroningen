using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Emando.Vantage.Entities.Identity
{
    public class VantageRole : IdentityRole<string, VantageUserRole>
    {
        public VantageRole()
        {
        }

        public VantageRole(string roleName)
        {
            Name = roleName;
        }

        public VantageRoleLevel Level { get; set; }
    }
}