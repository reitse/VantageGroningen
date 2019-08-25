namespace Emando.Vantage.Competitions
{
    public interface IRaceResult
    {
        TimeInvalidReason? TimeInvalidReason { get; }

        decimal? Points { get; }
    }
}