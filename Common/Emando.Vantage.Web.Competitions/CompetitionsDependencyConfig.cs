using System.Resources;
using Emando.Vantage.Web.Competitions.Resources;
using Microsoft.Practices.Unity;

namespace Emando.Vantage.Web.Competitions
{
    public static class CompetitionsDependencyConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterInstance("Web", new ResourceManager(typeof(Strings)));
        }
    }
}