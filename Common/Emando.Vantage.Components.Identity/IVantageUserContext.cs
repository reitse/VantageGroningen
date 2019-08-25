using System;
using System.Data;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Entities.Identity;

namespace Emando.Vantage.Components.Identity
{
    public interface IVantageUserContext : IDisposable
    {
        IContextTransaction BeginTransaction(IsolationLevel isolationLevel);

        IContextTransaction UseOrBeginTransaction(IsolationLevel isolationLevel);

        IDbSet<ClientApplication> ClientApplications { get; }
        
        IDbSet<ClientApplicationCompetitionRight> ClientApplicationCompetitionRights { get; }

        IDbSet<RefreshToken> RefreshTokens { get; }

        IDbSet<VantageUser> Users { get; }

        IDbSet<VantageRole> Roles { get; }

        IDbSet<VantageUserCompetitionRight> UserCompetitionRights { get; }

        IDbSet<UserSetting> UserSettings { get; } 

        bool LazyLoadingEnabled { get; set; }

        bool ProxyCreationEnabled { get; set; }

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}