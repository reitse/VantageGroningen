using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Competitions.Competitions",
                c => new
                {
                    Id = c.Guid(false),
                    SerieId = c.Guid(),
                    VenueCode = c.String(maxLength: 50),
                    Discipline = c.String(false, 100),
                    Location = c.String(maxLength: 200),
                    LocationFlags = c.Int(false),
                    Extra = c.String(),
                    Sponsor = c.String(maxLength: 100),
                    Name = c.String(false, 100),
                    Class = c.Int(false),
                    LicenseIssuerId = c.String(false, 50),
                    Starts = c.DateTime(false, 7, storeType: "datetime2"),
                    Ends = c.DateTime(precision: 7, storeType: "datetime2"),
                    Culture = c.String(maxLength: 10)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LicenseIssuers", t => t.LicenseIssuerId)
                .ForeignKey("Competitions.CompetitionSeries", t => t.SerieId)
                .ForeignKey("dbo.Venues", t => new
                {
                    t.VenueCode,
                    t.Discipline
                })
                .Index(t => t.SerieId)
                .Index(t => new
                {
                    t.VenueCode,
                    t.Discipline
                })
                .Index(t => t.LicenseIssuerId);

            CreateTable(
                "Competitions.CompetitorLists",
                c => new
                {
                    Id = c.Guid(false),
                    CompetitionId = c.Guid(false),
                    Name = c.String(false, 100),
                    SortOrder = c.Int(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Competitions.Competitions", t => t.CompetitionId, true)
                .Index(t => new
                {
                    t.CompetitionId,
                    t.Name
                }, unique: true, name: "UK_CompetitorLists_CompetitionId_Name");

            CreateTable(
                "Competitions.Competitors",
                c => new
                {
                    Id = c.Guid(false),
                    ListId = c.Guid(false),
                    StartNumber = c.Int(false),
                    EntityId = c.Guid(false),
                    ShortName = c.String(false, 50),
                    NationalityCode = c.String(false, 3),
                    LicenseKey = c.String(maxLength: 100),
                    Category = c.String(maxLength: 100),
                    Class = c.Int(),
                    Sponsor = c.String(maxLength: 100),
                    Club = c.String(maxLength: 100),
                    From = c.String(maxLength: 100),
                    Gender = c.Int(false),
                    Status = c.Int(false),
                    Transponder1 = c.String(maxLength: 50),
                    Transponder2 = c.String(maxLength: 50),
                    Added = c.DateTime(false, 7, storeType: "datetime2"),
                    Source = c.Int(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Competitions.CompetitorLists", t => t.ListId, true)
                .ForeignKey("dbo.Countries", t => t.NationalityCode)
                .Index(t => new
                {
                    t.ListId,
                    t.EntityId
                }, unique: true, name: "UK_Competitors_ListId_EntityId")
                .Index(t => new
                {
                    t.ListId,
                    t.StartNumber
                }, unique: true, name: "UK_Competitors_ListId_StartNumber")
                .Index(t => t.NationalityCode);

            CreateTable(
                "Competitions.DistanceCombinationCompetitors",
                c => new
                {
                    DistanceCombinationId = c.Guid(false),
                    CompetitorId = c.Guid(false),
                    Reserve = c.Int(),
                    Status = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.DistanceCombinationId,
                    t.CompetitorId
                })
                .ForeignKey("Competitions.Competitors", t => t.CompetitorId, true)
                .ForeignKey("Competitions.DistanceCombinations", t => t.DistanceCombinationId)
                .Index(t => t.DistanceCombinationId)
                .Index(t => t.CompetitorId);

            CreateTable(
                "Competitions.DistanceCombinations",
                c => new
                {
                    Id = c.Guid(false),
                    CompetitionId = c.Guid(false),
                    Number = c.Int(false),
                    Name = c.String(false, 100),
                    Starts = c.DateTime(precision: 7, storeType: "datetime2"),
                    Class = c.Int(false),
                    CategoryFilter = c.String(false, 100)
                })
                .PrimaryKey(t => t.Id, clustered: false)
                .ForeignKey("Competitions.Competitions", t => t.CompetitionId, true)
                .Index(t => new
                {
                    t.CompetitionId,
                    t.Name
                }, unique: true, name: "UK_DistanceCombinations_CompetitionId_Name")
                .Index(t => new
                {
                    t.CompetitionId,
                    t.Number
                }, unique: true, clustered: true, name: "UK_DistanceCombinations_CompetitionId_Number");

            CreateTable(
                "Competitions.Distances",
                c => new
                {
                    Id = c.Guid(false),
                    Discipline = c.String(false, 100),
                    CompetitionId = c.Guid(false),
                    DistancePointsTableId = c.Guid(),
                    Number = c.Int(false),
                    TrackLength = c.Decimal(false, 18, 3),
                    Value = c.Int(false),
                    Rounds = c.Int(false),
                    ValueQuantity = c.Int(false),
                    ClassificationWeight = c.Int(false),
                    ClassificationPrecision = c.Time(false, 7),
                    Name = c.String(false, 100),
                    Starts = c.DateTime(precision: 7, storeType: "datetime2"),
                    StartMode = c.Int(false),
                    FirstHeat = c.Int(false),
                    ContinuousNumbering = c.Boolean(false),
                    StartWeather_AirTemperature = c.Double(),
                    StartWeather_TrackTemperature = c.Double(),
                    StartWeather_WindSpeed = c.Double(),
                    StartWeather_Humidity = c.Double(),
                    StartWeather_AirPressure = c.Double(),
                    EndWeather_AirTemperature = c.Double(),
                    EndWeather_TrackTemperature = c.Double(),
                    EndWeather_WindSpeed = c.Double(),
                    EndWeather_Humidity = c.Double(),
                    EndWeather_AirPressure = c.Double()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Competitions.Competitions", t => t.CompetitionId, true)
                .ForeignKey("Competitions.DistancePointsTables", t => t.DistancePointsTableId)
                .Index(t => new
                {
                    t.CompetitionId,
                    t.Number
                }, unique: true, name: "UK_Distances_CompetitionId_Number")
                .Index(t => t.DistancePointsTableId);

            CreateTable(
                "Competitions.DistancePointsTables",
                c => new
                {
                    Id = c.Guid(false),
                    DistanceId = c.Guid(),
                    Name = c.String(false, 100)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DistanceId);

            CreateTable(
                "Competitions.DistancePoints",
                c => new
                {
                    DistancePointsTableId = c.Guid(false),
                    Ranking = c.Int(false),
                    Type = c.String(false, 100),
                    Points = c.Decimal(false, 18, 3)
                })
                .PrimaryKey(t => new
                {
                    t.DistancePointsTableId,
                    t.Ranking,
                    t.Type
                })
                .ForeignKey("Competitions.DistancePointsTables", t => t.DistancePointsTableId, true)
                .Index(t => t.DistancePointsTableId);

            CreateTable(
                "Competitions.Races",
                c => new
                {
                    Id = c.Guid(false),
                    DistanceId = c.Guid(false),
                    CompetitorId = c.Guid(false),
                    Round = c.Int(false),
                    Heat = c.Int(false),
                    Lane = c.Int(false),
                    Color = c.Int(false),
                    PresentedInstanceName = c.String(maxLength: 50),
                    PersonalBest = c.Time(precision: 7),
                    SeasonBest = c.Time(precision: 7)
                })
                .PrimaryKey(t => t.Id, clustered: false)
                .ForeignKey("Competitions.Competitors", t => t.CompetitorId)
                .ForeignKey("Competitions.Distances", t => t.DistanceId, true)
                .Index(t => new
                {
                    t.DistanceId,
                    t.Round,
                    t.Heat,
                    t.Lane
                }, unique: true, clustered: true, name: "UK_Races_DistanceId_Round_Heat_Lane")
                .Index(t => t.CompetitorId);

            CreateTable(
                "Competitions.RaceLaps",
                c => new
                {
                    RaceId = c.Guid(false),
                    ApplianceName = c.String(false, 50),
                    ApplianceInstanceName = c.String(false, 50),
                    InstanceName = c.String(false, 50),
                    How = c.String(false, 50),
                    Time = c.Time(false, 7),
                    When = c.DateTime(false, 7, storeType: "datetime2"),
                    Flags = c.Int(false),
                    Points = c.Decimal(precision: 18, scale: 3),
                    FixedIndex = c.Int(),
                    FixedRanking = c.Int()
                })
                .PrimaryKey(t => new
                {
                    t.RaceId,
                    t.ApplianceName,
                    t.ApplianceInstanceName,
                    t.InstanceName,
                    t.How,
                    t.Time
                })
                .ForeignKey("Competitions.Races", t => t.RaceId, true)
                .Index(t => t.RaceId)
                .Index(t => new
                {
                    t.RaceId,
                    t.InstanceName
                }, "IX_RaceLaps_RaceId_InstanceName");

            CreateTable(
                "Competitions.RacePassings",
                c => new
                {
                    ApplianceName = c.String(false, 50),
                    ApplianceInstanceName = c.String(false, 50),
                    InstanceName = c.String(false, 50),
                    RaceId = c.Guid(false),
                    How = c.String(false, 50),
                    When = c.DateTime(false, 7, storeType: "datetime2"),
                    Where = c.Long(false),
                    Flags = c.Int(false),
                    Time = c.Time(false, 7),
                    Passed = c.Decimal(precision: 18, scale: 3),
                    Speed = c.Decimal(precision: 18, scale: 3)
                })
                .PrimaryKey(t => new
                {
                    t.ApplianceName,
                    t.ApplianceInstanceName,
                    t.InstanceName,
                    t.RaceId,
                    t.How,
                    t.When
                })
                .ForeignKey("Competitions.Races", t => t.RaceId, true)
                .Index(t => new
                {
                    t.RaceId,
                    t.InstanceName
                }, "IX_RacePassings_RaceId_InstanceName")
                .Index(t => t.RaceId);

            CreateTable(
                "Competitions.RaceResults",
                c => new
                {
                    RaceId = c.Guid(false),
                    InstanceName = c.String(false, 50),
                    Points = c.Decimal(precision: 18, scale: 3),
                    Status = c.Int(false),
                    TimeInvalidReason = c.Int()
                })
                .PrimaryKey(t => new
                {
                    t.RaceId,
                    t.InstanceName
                })
                .ForeignKey("Competitions.Races", t => t.RaceId, true)
                .Index(t => t.RaceId);

            CreateTable(
                "Competitions.RaceStarts",
                c => new
                {
                    ApplianceName = c.String(false, 50),
                    ApplianceInstanceName = c.String(false, 50),
                    ApplianceEventId = c.Long(false),
                    InstanceName = c.String(false, 50),
                    RaceId = c.Guid(false),
                    How = c.String(false, 50),
                    When = c.DateTime(false, 7, storeType: "datetime2"),
                    Flags = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.ApplianceName,
                    t.ApplianceInstanceName,
                    t.ApplianceEventId,
                    t.InstanceName,
                    t.RaceId
                })
                .ForeignKey("Competitions.Races", t => t.RaceId, true)
                .Index(t => new
                {
                    t.RaceId,
                    t.InstanceName
                }, "IX_RaceStarts_RaceId_InstanceName")
                .Index(t => t.RaceId);

            CreateTable(
                "Competitions.RaceTimes",
                c => new
                {
                    RaceId = c.Guid(false),
                    InstanceName = c.String(false, 50),
                    ApplianceName = c.String(maxLength: 50),
                    ApplianceInstanceName = c.String(maxLength: 50),
                    How = c.String(maxLength: 50),
                    Time = c.Time(false, 7),
                    TimeInfo = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.RaceId,
                    t.InstanceName
                })
                .ForeignKey("Competitions.Races", t => t.RaceId, true)
                .Index(t => t.RaceId);

            CreateTable(
                "Competitions.RaceTransponders",
                c => new
                {
                    RaceId = c.Guid(false),
                    Type = c.String(false, 100),
                    Code = c.Long(false),
                    PersonId = c.Guid(false)
                })
                .PrimaryKey(t => new
                {
                    t.RaceId,
                    t.Type,
                    t.Code
                })
                .ForeignKey("dbo.People", t => t.PersonId)
                .ForeignKey("Competitions.Races", t => t.RaceId, true)
                .ForeignKey("dbo.Transponders", t => new
                {
                    t.Type,
                    t.Code
                }, true)
                .Index(t => t.RaceId)
                .Index(t => new
                {
                    t.Type,
                    t.Code
                })
                .Index(t => t.PersonId);

            CreateTable(
                "Competitions.TeamCompetitorMembers",
                c => new
                {
                    TeamId = c.Guid(false),
                    MemberId = c.Guid(false),
                    Order = c.Int(false),
                    Reserve = c.Int()
                })
                .PrimaryKey(t => new
                {
                    t.TeamId,
                    t.MemberId
                })
                .ForeignKey("Competitions.PersonCompetitors", t => t.MemberId)
                .ForeignKey("Competitions.TeamCompetitors", t => t.TeamId, true)
                .Index(t => new
                {
                    t.TeamId,
                    t.Order
                }, unique: true, name: "UK_TeamCompetitorMembers_TeamId_Order")
                .Index(t => t.MemberId);

            CreateTable(
                "Competitions.CompetitionSeries",
                c => new
                {
                    Id = c.Guid(false),
                    LicenseIssuerId = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    Season = c.Int(false),
                    Name = c.String(false, 100)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LicenseIssuers", t => t.LicenseIssuerId)
                .Index(t => t.LicenseIssuerId);

            CreateTable(
                "Competitions.DistanceDrawSettings",
                c => new
                {
                    CompetitionId = c.Guid(false),
                    GroupSize = c.Int(false),
                    ReverseGroups = c.Boolean(false),
                    Mode = c.Int(false),
                    ReverseFilling = c.Boolean(false),
                    Selectors = c.String(),
                    Spreading = c.Int(false)
                })
                .PrimaryKey(t => t.CompetitionId)
                .ForeignKey("Competitions.Competitions", t => t.CompetitionId, true)
                .Index(t => t.CompetitionId);

            CreateTable(
                "Competitions.PersonTimes",
                c => new
                {
                    PersonId = c.Guid(false),
                    VenueCode = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    DistanceDiscipline = c.String(false, 100),
                    Distance = c.Int(false),
                    Date = c.DateTime(false, storeType: "date"),
                    Time = c.Time(false, 7),
                    CompetitionId = c.Guid()
                })
                .PrimaryKey(t => new
                {
                    t.PersonId,
                    t.VenueCode,
                    t.Discipline,
                    t.DistanceDiscipline,
                    t.Distance,
                    t.Date,
                    t.Time
                })
                .ForeignKey("Competitions.Competitions", t => t.CompetitionId)
                .ForeignKey("dbo.People", t => t.PersonId, true)
                .ForeignKey("dbo.Venues", t => new
                {
                    t.VenueCode,
                    t.Discipline
                }, true)
                .Index(t => t.PersonId)
                .Index(t => new
                {
                    t.VenueCode,
                    t.Discipline
                })
                .Index(t => t.CompetitionId);

            CreateTable(
                "Competitions.ValidDistances",
                c => new
                {
                    Discipline = c.String(false, 100),
                    Value = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.Discipline,
                    t.Value
                });

            CreateTable(
                "Competitions.DistanceCombinationsDistances",
                c => new
                {
                    DistanceCombinationId = c.Guid(false),
                    DistanceId = c.Guid(false)
                })
                .PrimaryKey(t => new
                {
                    t.DistanceCombinationId,
                    t.DistanceId
                })
                .ForeignKey("Competitions.DistanceCombinations", t => t.DistanceCombinationId, true)
                .ForeignKey("Competitions.Distances", t => t.DistanceId)
                .Index(t => t.DistanceCombinationId)
                .Index(t => t.DistanceId);

            CreateTable(
                "Competitions.PersonCompetitorLists",
                c => new
                {
                    Id = c.Guid(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Competitions.CompetitorLists", t => t.Id, true)
                .Index(t => t.Id);

            CreateTable(
                "Competitions.PersonCompetitors",
                c => new
                {
                    Id = c.Guid(false),
                    Name_Initials = c.String(maxLength: 20),
                    Name_FirstName = c.String(maxLength: 100),
                    Name_SurnamePrefix = c.String(maxLength: 20),
                    Name_Surname = c.String(maxLength: 100),
                    PersonId = c.Guid(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Competitions.Competitors", t => t.Id, true)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.Id)
                .Index(t => t.PersonId);

            CreateTable(
                "Competitions.TeamCompetitorLists",
                c => new
                {
                    Id = c.Guid(false),
                    PersonsId = c.Guid(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Competitions.CompetitorLists", t => t.Id, true)
                .ForeignKey("Competitions.PersonCompetitorLists", t => t.PersonsId)
                .Index(t => t.Id)
                .Index(t => t.PersonsId);

            CreateTable(
                "Competitions.TeamCompetitors",
                c => new
                {
                    Id = c.Guid(false),
                    Name = c.String(false, 100)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Competitions.Competitors", t => t.Id, true)
                .Index(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("Competitions.TeamCompetitors", "Id", "Competitions.Competitors");
            DropForeignKey("Competitions.TeamCompetitorLists", "PersonsId", "Competitions.PersonCompetitorLists");
            DropForeignKey("Competitions.TeamCompetitorLists", "Id", "Competitions.CompetitorLists");
            DropForeignKey("Competitions.PersonCompetitors", "PersonId", "dbo.People");
            DropForeignKey("Competitions.PersonCompetitors", "Id", "Competitions.Competitors");
            DropForeignKey("Competitions.PersonCompetitorLists", "Id", "Competitions.CompetitorLists");
            DropForeignKey("Competitions.PersonTimes", new[] { "VenueCode", "Discipline" }, "dbo.Venues");
            DropForeignKey("Competitions.PersonTimes", "PersonId", "dbo.People");
            DropForeignKey("Competitions.PersonTimes", "CompetitionId", "Competitions.Competitions");
            DropForeignKey("Competitions.DistanceDrawSettings", "CompetitionId", "Competitions.Competitions");
            DropForeignKey("Competitions.Competitions", new[] { "VenueCode", "Discipline" }, "dbo.Venues");
            DropForeignKey("Competitions.CompetitionSeries", "LicenseIssuerId", "dbo.LicenseIssuers");
            DropForeignKey("Competitions.Competitions", "SerieId", "Competitions.CompetitionSeries");
            DropForeignKey("Competitions.Competitions", "LicenseIssuerId", "dbo.LicenseIssuers");
            DropForeignKey("Competitions.TeamCompetitorMembers", "TeamId", "Competitions.TeamCompetitors");
            DropForeignKey("Competitions.TeamCompetitorMembers", "MemberId", "Competitions.PersonCompetitors");
            DropForeignKey("Competitions.Competitors", "NationalityCode", "dbo.Countries");
            DropForeignKey("Competitions.Competitors", "ListId", "Competitions.CompetitorLists");
            DropForeignKey("Competitions.DistanceCombinationsDistances", "DistanceId", "Competitions.Distances");
            DropForeignKey("Competitions.DistanceCombinationsDistances", "DistanceCombinationId", "Competitions.DistanceCombinations");
            DropForeignKey("Competitions.RaceTransponders", new[] { "Type", "Code" }, "dbo.Transponders");
            DropForeignKey("Competitions.RaceTransponders", "RaceId", "Competitions.Races");
            DropForeignKey("Competitions.RaceTransponders", "PersonId", "dbo.People");
            DropForeignKey("Competitions.RaceTimes", "RaceId", "Competitions.Races");
            DropForeignKey("Competitions.RaceStarts", "RaceId", "Competitions.Races");
            DropForeignKey("Competitions.RaceResults", "RaceId", "Competitions.Races");
            DropForeignKey("Competitions.RacePassings", "RaceId", "Competitions.Races");
            DropForeignKey("Competitions.RaceLaps", "RaceId", "Competitions.Races");
            DropForeignKey("Competitions.Races", "DistanceId", "Competitions.Distances");
            DropForeignKey("Competitions.Races", "CompetitorId", "Competitions.Competitors");
            DropForeignKey("Competitions.Distances", "DistancePointsTableId", "Competitions.DistancePointsTables");
            DropForeignKey("Competitions.DistancePoints", "DistancePointsTableId", "Competitions.DistancePointsTables");
            DropForeignKey("Competitions.Distances", "CompetitionId", "Competitions.Competitions");
            DropForeignKey("Competitions.DistanceCombinationCompetitors", "DistanceCombinationId", "Competitions.DistanceCombinations");
            DropForeignKey("Competitions.DistanceCombinations", "CompetitionId", "Competitions.Competitions");
            DropForeignKey("Competitions.DistanceCombinationCompetitors", "CompetitorId", "Competitions.Competitors");
            DropForeignKey("Competitions.CompetitorLists", "CompetitionId", "Competitions.Competitions");
            DropIndex("Competitions.TeamCompetitors", new[] { "Id" });
            DropIndex("Competitions.TeamCompetitorLists", new[] { "PersonsId" });
            DropIndex("Competitions.TeamCompetitorLists", new[] { "Id" });
            DropIndex("Competitions.PersonCompetitors", new[] { "PersonId" });
            DropIndex("Competitions.PersonCompetitors", new[] { "Id" });
            DropIndex("Competitions.PersonCompetitorLists", new[] { "Id" });
            DropIndex("Competitions.DistanceCombinationsDistances", new[] { "DistanceId" });
            DropIndex("Competitions.DistanceCombinationsDistances", new[] { "DistanceCombinationId" });
            DropIndex("Competitions.PersonTimes", new[] { "CompetitionId" });
            DropIndex("Competitions.PersonTimes", new[] { "VenueCode", "Discipline" });
            DropIndex("Competitions.PersonTimes", new[] { "PersonId" });
            DropIndex("Competitions.DistanceDrawSettings", new[] { "CompetitionId" });
            DropIndex("Competitions.CompetitionSeries", new[] { "LicenseIssuerId" });
            DropIndex("Competitions.TeamCompetitorMembers", new[] { "MemberId" });
            DropIndex("Competitions.TeamCompetitorMembers", "UK_TeamCompetitorMembers_TeamId_Order");
            DropIndex("Competitions.RaceTransponders", new[] { "PersonId" });
            DropIndex("Competitions.RaceTransponders", new[] { "Type", "Code" });
            DropIndex("Competitions.RaceTransponders", new[] { "RaceId" });
            DropIndex("Competitions.RaceTimes", new[] { "RaceId" });
            DropIndex("Competitions.RaceStarts", new[] { "RaceId" });
            DropIndex("Competitions.RaceStarts", "IX_RaceStarts_RaceId_InstanceName");
            DropIndex("Competitions.RaceResults", new[] { "RaceId" });
            DropIndex("Competitions.RacePassings", new[] { "RaceId" });
            DropIndex("Competitions.RacePassings", "IX_RacePassings_RaceId_InstanceName");
            DropIndex("Competitions.RaceLaps", "IX_RaceLaps_RaceId_InstanceName");
            DropIndex("Competitions.RaceLaps", new[] { "RaceId" });
            DropIndex("Competitions.Races", new[] { "CompetitorId" });
            DropIndex("Competitions.Races", "UK_Races_DistanceId_Round_Heat_Lane");
            DropIndex("Competitions.DistancePoints", new[] { "DistancePointsTableId" });
            DropIndex("Competitions.DistancePointsTables", new[] { "DistanceId" });
            DropIndex("Competitions.Distances", new[] { "DistancePointsTableId" });
            DropIndex("Competitions.Distances", "UK_Distances_CompetitionId_Number");
            DropIndex("Competitions.DistanceCombinations", "UK_DistanceCombinations_CompetitionId_Number");
            DropIndex("Competitions.DistanceCombinations", "UK_DistanceCombinations_CompetitionId_Name");
            DropIndex("Competitions.DistanceCombinationCompetitors", new[] { "CompetitorId" });
            DropIndex("Competitions.DistanceCombinationCompetitors", new[] { "DistanceCombinationId" });
            DropIndex("Competitions.Competitors", new[] { "NationalityCode" });
            DropIndex("Competitions.Competitors", "UK_Competitors_ListId_StartNumber");
            DropIndex("Competitions.Competitors", "UK_Competitors_ListId_EntityId");
            DropIndex("Competitions.CompetitorLists", "UK_CompetitorLists_CompetitionId_Name");
            DropIndex("Competitions.Competitions", new[] { "LicenseIssuerId" });
            DropIndex("Competitions.Competitions", new[] { "VenueCode", "Discipline" });
            DropIndex("Competitions.Competitions", new[] { "SerieId" });
            DropTable("Competitions.TeamCompetitors");
            DropTable("Competitions.TeamCompetitorLists");
            DropTable("Competitions.PersonCompetitors");
            DropTable("Competitions.PersonCompetitorLists");
            DropTable("Competitions.DistanceCombinationsDistances");
            DropTable("Competitions.ValidDistances");
            DropTable("Competitions.PersonTimes");
            DropTable("Competitions.DistanceDrawSettings");
            DropTable("Competitions.CompetitionSeries");
            DropTable("Competitions.TeamCompetitorMembers");
            DropTable("Competitions.RaceTransponders");
            DropTable("Competitions.RaceTimes");
            DropTable("Competitions.RaceStarts");
            DropTable("Competitions.RaceResults");
            DropTable("Competitions.RacePassings");
            DropTable("Competitions.RaceLaps");
            DropTable("Competitions.Races");
            DropTable("Competitions.DistancePoints");
            DropTable("Competitions.DistancePointsTables");
            DropTable("Competitions.Distances");
            DropTable("Competitions.DistanceCombinations");
            DropTable("Competitions.DistanceCombinationCompetitors");
            DropTable("Competitions.Competitors");
            DropTable("Competitions.CompetitorLists");
            DropTable("Competitions.Competitions");
        }
    }
}