using Microsoft.Practices.Unity;

namespace Emando.Vantage.Components.Competitions.Infrastructure
{
    public class UnityDisciplineCalculatorManager : IDisciplineCalculatorManager
    {
        private readonly IUnityContainer container;

        public UnityDisciplineCalculatorManager(IUnityContainer container)
        {
            this.container = container;
        }

        #region IDisciplineDistanceExpertManager Members

        public IDisciplineCalculator Find(string discipline)
        {
            return container.IsRegistered<IDisciplineCalculator>(discipline) ? container.Resolve<IDisciplineCalculator>(discipline) : null;
        }

        #endregion
    }
}