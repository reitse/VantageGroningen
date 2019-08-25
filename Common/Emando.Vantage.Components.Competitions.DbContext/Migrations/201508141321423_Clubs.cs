using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class Clubs : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Competitions", "ResultsStatus", c => c.Int(false));
            AddColumn("Competitions.Competitors", "ClubCountryCode", c => c.String());
            AddColumn("Competitions.Competitors", "ClubCode", c => c.Int());
            AddColumn("Competitions.Competitors", "ClubShortName", c => c.String(maxLength: 20));
            AddColumn("Competitions.Competitors", "ClubFullName", c => c.String(maxLength: 100));
            DropColumn("Competitions.Competitors", "Club");
        }

        public override void Down()
        {
            AddColumn("Competitions.Competitors", "Club", c => c.String(maxLength: 100));
            DropColumn("Competitions.Competitors", "ClubFullName");
            DropColumn("Competitions.Competitors", "ClubShortName");
            DropColumn("Competitions.Competitors", "ClubCode");
            DropColumn("Competitions.Competitors", "ClubCountryCode");
            DropColumn("Competitions.Competitions", "ResultsStatus");
        }
    }
}