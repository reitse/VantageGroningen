using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components.Identity;
using Emando.Vantage.Entities.Identity;

namespace Emando.Vantage.Workflows.Security
{
    public class RefreshTokensWorkflow : IDisposable
    {
        private readonly IVantageUserContext context;
        private bool isDisposed;

        public RefreshTokensWorkflow(IVantageUserContext context)
        {
            this.context = context;
        }

        public IQueryable<RefreshToken> RefreshTokens => context.RefreshTokens;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public async Task<RefreshToken> FindRefreshTokenAsync(string tokenHash)
        {
            return await context.RefreshTokens.FirstOrDefaultAsync(r => r.TokenHash == tokenHash);
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRefreshTokenAsync(RefreshToken refreshToken)
        {
            context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRefreshTokensAsync(string subject)
        {
            var refreshTokens = await context.RefreshTokens.Where(r => r.Subject == subject).ToListAsync();
            foreach (var refreshToken in refreshTokens)
                context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync();
        }

        public async Task CleanupRefreshTokensAsync()
        {
            foreach (var refreshToken in await context.RefreshTokens.Where(t => t.Expires <= DateTime.UtcNow).ToListAsync())
                context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    context.Dispose();

                isDisposed = true;
            }
        }

        ~RefreshTokensWorkflow()
        {
            Dispose(false);
        }
    }
}