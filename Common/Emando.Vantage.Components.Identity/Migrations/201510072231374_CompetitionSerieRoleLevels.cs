namespace Emando.Vantage.Components.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompetitionSerieRoleLevels : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [Users].AspNetRoles " +
                "SET [Level] = 0 " +
                "WHERE Name LIKE 'CompetitionSeries.%'");

            Sql("DELETE ur " +
                "FROM [Users].AspNetUserCompetitionRights ur " +
                "INNER JOIN [Users].AspNetRoles r ON ur.RoleId = r.Id " +
                "WHERE r.[Level] = 0");
        }
        
        public override void Down()
        {
        }
    }
}
