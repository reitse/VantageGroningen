using Microsoft.Practices.Unity;

namespace Emando.Vantage.Components.Competitions.Infrastructure
{
    public class UnityDistanceDisciplineCalculatorManager : IDistanceDisciplineCalculatorManager
    {
        private readonly IUnityContainer container;

        public UnityDistanceDisciplineCalculatorManager(IUnityContainer container)
        {
            this.container = container;
        }

        #region IDistanceDisciplineCalculatorManager Members

        public IDistanceDisciplineCalculator Get(string discipline)
        {
            return container.IsRegistered<IDistanceDisciplineCalculator>(discipline)
                ? container.Resolve<IDistanceDisciplineCalculator>(discipline)
                : new DistanceDisciplineCalculator();
        }

        #endregion
    }
}