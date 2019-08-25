namespace Emando.Vantage.Components.Competitions
{
    public interface IDistanceDisciplineCalculatorManager
    {
        IDistanceDisciplineCalculator Get(string discipline);
    }
}