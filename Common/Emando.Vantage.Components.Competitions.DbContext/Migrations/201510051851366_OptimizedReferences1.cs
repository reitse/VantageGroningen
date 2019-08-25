using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class OptimizedReferences1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Competitions.PersonTimes", "CompetitionId", "Competitions.Competitions");
        }

        public override void Down()
        {
            AddForeignKey("Competitions.PersonTimes", "CompetitionId", "Competitions.Competitions", "Id");
        }
    }
}