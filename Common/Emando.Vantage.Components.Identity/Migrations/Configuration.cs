using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Emando.Vantage.Components.Identity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<VantageUserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VantageUserContext context)
        {
#if TESTDATA
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(VantageRoles.Users.Registrar))
                roleManager.Create(new IdentityRole(VantageRoles.Users.Registrar));
            if (!roleManager.RoleExists(VantageRoles.PersonLicenses.Issuer))
                roleManager.Create(new IdentityRole(VantageRoles.PersonLicenses.Issuer));
            if (!roleManager.RoleExists(VantageRoles.PersonLicenses.DetailsViewer))
                roleManager.Create(new IdentityRole(VantageRoles.PersonLicenses.DetailsViewer));
            if (!roleManager.RoleExists(VantageRoles.Transponders.Administrator))
                roleManager.Create(new IdentityRole(VantageRoles.Transponders.Administrator));
            if (!roleManager.RoleExists(VantageRoles.CompetitionSeries.Administrator))
                roleManager.Create(new IdentityRole(VantageRoles.CompetitionSeries.Administrator));
            if (!roleManager.RoleExists(VantageRoles.CompetitionSeries.Editor))
                roleManager.Create(new IdentityRole(VantageRoles.CompetitionSeries.Editor));
            if (!roleManager.RoleExists(VantageRoles.Competitions.Administrator))
                roleManager.Create(new IdentityRole(VantageRoles.Competitions.Administrator));
            if (!roleManager.RoleExists(VantageRoles.Competitions.Editor))
                roleManager.Create(new IdentityRole(VantageRoles.Competitions.Editor));
            if (!roleManager.RoleExists(VantageRoles.Competitions.Registrar))
                roleManager.Create(new IdentityRole(VantageRoles.Competitions.Registrar));

            var userManager = new UserManager<VantageUser>(new UserStore<VantageUser>(context));
            var user = userManager.FindByName("admin@emandovantage.com");
            if (user == null)
            {
                user = new VantageUser
                {
                    UserName = "admin@emandovantage.com",
                    Name = new Name(),
                    Email = "admin@emandovantage.com",
                    EmailConfirmed = true
                };
                userManager.Create(user, "password");
                userManager.AddToRole(user.Id, VantageRoles.Users.Administrator);
                userManager.AddToRole(user.Id, VantageRoles.PersonLicenses.Issuer);
                userManager.AddToRole(user.Id, VantageRoles.PersonLicenses.DetailsViewer);
                userManager.AddToRole(user.Id, VantageRoles.Transponders.Administrator);
            }

            context.ClientApplications.AddOrUpdate(c => c.Id,
                new ClientApplication
                {
                    Id = "Manager",
                    Name = "Vantage Manager",
                    Secret = "password".ToSHA256Base64Hash(),
                    IsActive = true,
                    KnowsSecret = false,
                    RefreshTokenLifeTime = (int)TimeSpan.FromDays(60).TotalMinutes,
                    AllowedOrigin = "*"
                },
                new ClientApplication
                {
                    Id = "KNSBLicenseIssuer",
                    Name = "KNSB License Issuer",
                    Secret = "password".ToSHA256Base64Hash(),
                    IsActive = true,
                    KnowsSecret = true,
                    RefreshTokenLifeTime = (int)TimeSpan.FromDays(60).TotalMinutes,
                    AllowedOrigin = "*",
                    Roles = new Collection<IdentityRole>
                    {
                        roleManager.FindByName(VantageRoles.PersonLicenses.Issuer)
                    }
                },
                new ClientApplication
                {
                    Id = "KNSBRegistrations",
                    Name = "KNSB Registrations",
                    Secret = "password".ToSHA256Base64Hash(),
                    IsActive = true,
                    KnowsSecret = true,
                    RefreshTokenLifeTime = (int)TimeSpan.FromDays(60).TotalMinutes,
                    AllowedOrigin = "*",
                    Roles = new Collection<IdentityRole>
                    {
                        roleManager.FindByName(VantageRoles.Competitions.Registrar)
                    }
                });
#endif
        }
    }
}