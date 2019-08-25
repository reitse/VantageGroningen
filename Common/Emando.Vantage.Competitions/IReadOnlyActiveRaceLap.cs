namespace Emando.Vantage.Competitions
{
    public interface IReadOnlyActiveRaceLap : IReadOnlyRaceLap
    {
        int? Index { get; }

        int? Ranking { get; }
    }
}