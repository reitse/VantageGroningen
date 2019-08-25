using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public static class DistanceDisciplineCalculatorExtensions
    {
        public static RaceLapIndex LapIndex(this IDistanceDisciplineCalculator calculator, Distance distance, int index)
        {
            return new RaceLapIndex(index, calculator.Rounds(distance, index + 1), calculator.RoundsToGo(distance, index + 1), calculator.LapPassedLength(distance, index + 1));
        }
    }
}