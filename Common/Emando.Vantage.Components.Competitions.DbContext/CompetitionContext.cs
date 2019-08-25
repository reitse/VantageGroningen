using System.Data.Common;
using System.Data.Entity;
using Emando.Vantage.Components.Competitions.Migrations;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public class CompetitionContext : VantageContext, ICompetitionContext
    {
        private const string Schema = "Competitions";

        public CompetitionContext()
        {
        }

        public CompetitionContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CompetitionContext, Configuration>(true));
        }

        public CompetitionContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CompetitionContext, Configuration>(true));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompetitionSerie>().ToTable("CompetitionSeries", Schema);
            modelBuilder.Entity<Competition>().ToTable("Competitions", Schema);
            modelBuilder.Entity<Distance>().ToTable("Distances", Schema);
            modelBuilder.Entity<DistanceDrawSettings>().ToTable("DistanceDrawSettings", Schema);
            modelBuilder.Entity<DistancePointsTable>().ToTable("DistancePointsTables", Schema);
            modelBuilder.Entity<DistancePoints>().ToTable("DistancePoints", Schema);
            modelBuilder.Entity<ValidDistance>().ToTable("ValidDistances", Schema);
            modelBuilder.Entity<DistanceCombination>().ToTable("DistanceCombinations", Schema);
            modelBuilder.Entity<DistanceCombinationCompetitor>().ToTable("DistanceCombinationCompetitors", Schema);
            modelBuilder.Entity<CompetitorListBase>().ToTable("CompetitorLists", Schema);
            modelBuilder.Entity<CompetitorBase>().ToTable("Competitors", Schema);
            modelBuilder.Entity<PersonCompetitorList>().ToTable("PersonCompetitorLists", Schema);
            modelBuilder.Entity<PersonCompetitor>().ToTable("PersonCompetitors", Schema);
            modelBuilder.Entity<TeamCompetitorList>().ToTable("TeamCompetitorLists", Schema);
            modelBuilder.Entity<TeamCompetitor>().ToTable("TeamCompetitors", Schema);
            modelBuilder.Entity<TeamCompetitorMember>().ToTable("TeamCompetitorMembers", Schema);
            modelBuilder.Entity<Race>().ToTable("Races", Schema);
            modelBuilder.Entity<RaceTransponder>().ToTable("RaceTransponders", Schema);
            modelBuilder.Entity<RaceStart>().ToTable("RaceStarts", Schema);
            modelBuilder.Entity<RacePassing>().ToTable("RacePassings", Schema);
            modelBuilder.Entity<RaceLap>().ToTable("RaceLaps", Schema);
            modelBuilder.Entity<RaceResult>().ToTable("RaceResults", Schema);
            modelBuilder.Entity<RaceTime>().ToTable("RaceTimes", Schema);
            modelBuilder.Entity<PersonTime>().ToTable("PersonTimes", Schema);
            modelBuilder.Entity<RecordTime>().ToTable("RecordTimes", Schema);

            modelBuilder.Entity<RaceLap>()
                .Ignore(r => r.Index)
                .Ignore(r => r.Ranking)
                .Ignore(r => r.PresentationSource);
            modelBuilder.Entity<Race>()
                .Ignore(r => r.EstimatedLaps);

            modelBuilder.ComplexType<Weather>();

            modelBuilder.Entity<Competition>().HasOptional(c => c.Venue).WithMany().HasForeignKey(c => new
            {
                c.VenueCode,
                c.Discipline
            }).WillCascadeOnDelete(false);
            modelBuilder.Entity<Competition>().HasRequired(t => t.LicenseIssuer).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<CompetitionSerie>().HasRequired(t => t.LicenseIssuer).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<RaceTransponder>().HasRequired(t => t.Person).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<CompetitorBase>().HasRequired(t => t.Nationality).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PersonCompetitor>().HasRequired(t => t.Person).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<DistanceCombinationCompetitor>().HasRequired(t => t.Competitor).WithMany(t => t.DistanceCombinations).WillCascadeOnDelete(false);
            modelBuilder.Entity<DistanceCombination>()
                .HasMany(dc => dc.Distances)
                .WithMany(d => d.Combinations)
                .Map(c => c.ToTable("DistanceCombinationsDistances", Schema).MapLeftKey("DistanceCombinationId").MapRightKey("DistanceId"));
            modelBuilder.Entity<PersonTime>().HasRequired(t => t.Venue).WithMany().HasForeignKey(t => new
            {
                t.VenueCode,
                t.Discipline
            });
            modelBuilder.Entity<RecordTime>().HasRequired(t => t.Venue).WithMany().HasForeignKey(t => new
            {
                t.VenueCode,
                t.Discipline
            });
            modelBuilder.Entity<DistanceDrawSettings>().HasRequired(e => e.Competition).WithOptional().WillCascadeOnDelete(true);
            modelBuilder.Entity<Race>().HasRequired(r => r.Distance).WithMany(d => d.Races).WillCascadeOnDelete(true);
            modelBuilder.Entity<Race>().HasRequired(r => r.Competitor).WithMany(c => c.Races).WillCascadeOnDelete(false);
            modelBuilder.Entity<Competition>().HasOptional(c => c.ReportTemplate).WithMany().HasForeignKey(c => new
            {
                c.LicenseIssuerId,
                c.ReportTemplateName
            }).WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Distance>().Property(e => e.TrackLength).HasPrecision(18, 3);
            modelBuilder.Entity<DistancePoints>().Property(e => e.Points).HasPrecision(18, 3);
            modelBuilder.Entity<RaceLap>().Property(e => e.Points).HasPrecision(18, 3);
            modelBuilder.Entity<RacePassing>().Property(e => e.Passed).HasPrecision(18, 3);
            modelBuilder.Entity<RacePassing>().Property(e => e.Speed).HasPrecision(18, 3);
            modelBuilder.Entity<RaceResult>().Property(e => e.Points).HasPrecision(18, 3);
            modelBuilder.Entity<PersonTime>().Property(e => e.Date).HasColumnType("date");
            modelBuilder.Entity<RecordTime>().Property(e => e.Date).HasColumnType("date");

            base.OnModelCreating(modelBuilder);
        }

        #region ICompetitionContext Members

        public IDbSet<CompetitionSerie> CompetitionSeries { get; set; }

        public IDbSet<Competition> Competitions { get; set; }

        public IDbSet<TeamCompetitorMember> TeamCompetitorMembers { get; set; } 

        public IDbSet<Distance> Distances { get; set; }

        public IDbSet<DistanceDrawSettings> DistanceDrawSettings { get; set; }

        public IDbSet<DistancePointsTable> DistancePointsTables { get; set; }

        public IDbSet<DistancePoints> DistancePoints { get; set; }

        public IDbSet<ValidDistance> ValidDistances { get; set; }

        public IDbSet<DistanceCombination> DistanceCombinations { get; set; }

        public IDbSet<DistanceCombinationCompetitor> DistanceCombinationCompetitors { get; set; }

        public IDbSet<CompetitorListBase> CompetitorLists { get; set; }

        public IDbSet<CompetitorBase> Competitors { get; set; }

        public IDbSet<Race> Races { get; set; }

        public IDbSet<RaceTransponder> RaceTransponders { get; set; }

        public IDbSet<RaceStart> RaceStarts { get; set; }

        public IDbSet<RacePassing> RacePassings { get; set; }

        public IDbSet<RaceLap> RaceLaps { get; set; }

        public IDbSet<RaceTime> RaceTimes { get; set; }

        public IDbSet<RaceResult> RaceResults { get; set; }

        public IDbSet<PersonTime> PersonTimes { get; set; }

        public IDbSet<RecordTime> RecordTimes { get; set; } 

        #endregion
    }
}