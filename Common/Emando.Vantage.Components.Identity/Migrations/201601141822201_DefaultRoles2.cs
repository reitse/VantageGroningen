namespace Emando.Vantage.Components.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultRoles2 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [Users].AspNetRoles (Id, Name) VALUES " +
                "('daa6aba9-7995-4799-b873-64bc143576ab', 'Reports.TemplateEditor'), " +
                "('b809283a-dddd-4138-8bb2-9a1a4611d962', 'Competitions.DistancePointsEditor')");

            Sql("INSERT INTO [Users].AspNetUserRoles (UserId, RoleId) " +
                "SELECT DISTINCT ur.UserId, nr.Id "
                + "FROM [Users].AspNetUserRoles ur "
                + "INNER JOIN [Users].AspNetUserCompetitionRights cr ON ur.UserId = cr.UserId "
                + "INNER JOIN [Users].AspNetRoles crr ON cr.RoleId = crr.Id "
                + "INNER JOIN [Users].AspNetRoles nr ON (nr.Name = 'Competitions.DistancePointsEditor' OR nr.Name = 'Reports.TemplateEditor') "
                + "WHERE crr.Name = 'Competitions.Administrator' "
                + "AND (SELECT COUNT(*) FROM [Users].AspNetUserRoles WHERE UserId = ur.UserId AND RoleId = nr.Id) = 0");
        }

        public override void Down()
        {
        }
    }
}
