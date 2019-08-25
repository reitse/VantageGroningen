using System;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components.Identity;
using Emando.Vantage.Entities.Identity;

namespace Emando.Vantage.Workflows.Security
{
    public class UserSettingsWorkflow : IDisposable
    {
        private readonly IVantageUserContext context;
        private bool isDisposed;

        public UserSettingsWorkflow(IVantageUserContext context)
        {
            this.context = context;
        }

        public IQueryable<UserSetting> Settings(string userId, string type)
        {
            return context.UserSettings.Where(s => s.UserId == userId && s.Type == type);
        }

        public async Task AddAsync(UserSetting setting)
        {
            context.UserSettings.Add(setting);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(UserSetting setting)
        {
            context.UserSettings.Remove(setting);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSetting setting)
        {
            await context.SaveChangesAsync();
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~UserSettingsWorkflow()
        {
            Dispose(false);
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
    }
}