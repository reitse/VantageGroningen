using System.Data;
using System.Data.Entity;
using Emando.Vantage.Components.Identity.Migrations;
using Emando.Vantage.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Emando.Vantage.Components.Identity
{
    public class VantageUserContext : IdentityDbContext<VantageUser, VantageRole, string, IdentityUserLogin, VantageUserRole, IdentityUserClaim>,
        IContextInitializer, IVantageUserContext
    {
        private IContextTransaction transaction;

        public VantageUserContext() : this("ConnectionString")
        {
        }

        public VantageUserContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            LazyLoadingEnabled = false;
            ProxyCreationEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VantageUserContext, Configuration>(nameOrConnectionString));
        }

        public IContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            transaction = new DbContextTransactionProxy(Database.BeginTransaction(isolationLevel));
            return transaction;
        }

        public IContextTransaction UseOrBeginTransaction(IsolationLevel isolationLevel)
        {
            return transaction != null
                ? new NestedContextTransaction(transaction)
                : BeginTransaction(isolationLevel);
        }

        #region IContextInitializer Members

        public void Initialize(bool force)
        {
            Database.Initialize(force);
        }

        #endregion

        #region IVantageUserContext Members

        public IDbSet<ClientApplication> ClientApplications { get; set; }

        public IDbSet<ClientApplicationCompetitionRight> ClientApplicationCompetitionRights { get; set; }

        public IDbSet<RefreshToken> RefreshTokens { get; set; }

        public IDbSet<VantageUserCompetitionRight> UserCompetitionRights { get; set; } 

        public IDbSet<UserSetting> UserSettings { get; set; } 

        public bool LazyLoadingEnabled
        {
            get { return Configuration.LazyLoadingEnabled; }
            set { Configuration.LazyLoadingEnabled = value; }
        }

        public bool ProxyCreationEnabled
        {
            get { return Configuration.ProxyCreationEnabled; }
            set { Configuration.ProxyCreationEnabled = value; }
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Users");

            modelBuilder.ComplexType<Name>();

            modelBuilder.Entity<ClientApplication>()
                .HasMany(c => c.Roles)
                .WithMany()
                .Map(o => o.MapLeftKey("ClientApplicationId").MapRightKey("RoleId").ToTable("ClientApplicationRoles"));

            modelBuilder.Entity<VantageUserRole>().ToTable("AspNetUserRoles").HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<VantageUserCompetitionRight>().ToTable("AspNetUserCompetitionRights");
        }
    }
}