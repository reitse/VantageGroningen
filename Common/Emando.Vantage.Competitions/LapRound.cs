namespace Emando.Vantage.Competitions
{
    public struct LapRound
    {
        public LapRound(decimal rounds, decimal roundsToGo, int passedLength)
        {
            Rounds = rounds;
            RoundsToGo = roundsToGo;
            PassedLength = passedLength;
        }

        public decimal Rounds { get; }

        public decimal RoundsToGo { get; }

        public int PassedLength { get; }
    }
}