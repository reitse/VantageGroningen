using System.Windows.Media;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions
{
    public interface IDistanceDisciplinePathProvider
    {
        IDistanceDisciplineCalculator Calculator { get; }

        PathGeometry CreatePath(Distance distance, int lane);
    }
}