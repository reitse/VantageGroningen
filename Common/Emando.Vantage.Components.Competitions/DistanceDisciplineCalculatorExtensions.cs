using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public static class DistanceDisciplineCalculatorExtensions
    {
        public static LapRound LapRound(this IDistanceDisciplineCalculator calculator, IDistance distance, int lap)
        {
            return new LapRound(calculator.Rounds(distance, lap), calculator.RoundsToGo(distance, lap), calculator.LapPassedLength(distance, lap));
        }
    }
}