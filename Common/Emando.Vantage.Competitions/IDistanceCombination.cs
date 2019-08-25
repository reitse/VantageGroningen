namespace Emando.Vantage.Competitions
{
    public interface IDistanceCombination
    {
        int Number { get; }

        string ClassFilter { get; }

        string CategoryFilter { get; }
    }
}