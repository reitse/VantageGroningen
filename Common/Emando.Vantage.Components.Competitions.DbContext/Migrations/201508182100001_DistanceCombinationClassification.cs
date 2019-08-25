using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class DistanceCombinationClassification : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.DistanceCombinations", "ClassificationWeight", c => c.Int(false, defaultValue: 500));
            DropColumn("Competitions.Distances", "ClassificationWeight");
        }

        public override void Down()
        {
            AddColumn("Competitions.Distances", "ClassificationWeight", c => c.Int(false, defaultValue: 500));
            DropColumn("Competitions.DistanceCombinations", "ClassificationWeight");
        }
    }
}