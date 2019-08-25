using System.Linq;
using Microsoft.Practices.Unity;

namespace Emando.Vantage.Workflows.Competitions.Infrastructure
{
    public class UnityDistanceDisciplineExpertManager : IDistanceDisciplineExpertManager
    {
        private readonly IUnityContainer container;

        public UnityDistanceDisciplineExpertManager(IUnityContainer container)
        {
            this.container = container;
        }

        #region IDisciplineDistanceExpertManager Members

        public IDistanceDisciplineExpert Find(string discipline)
        {
            return container.IsRegistered<IDistanceDisciplineExpert>(discipline) ? container.Resolve<IDistanceDisciplineExpert>(discipline) : null;
        }

        public string[] GetKeys()
        {
            return container.Registrations.Where(r => r.RegisteredType == typeof(IDistanceDisciplineExpert)).Select(r => r.Name).ToArray();
        }

        #endregion
    }
}