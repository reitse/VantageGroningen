using System;

namespace Emando.Vantage.Windows.Competitions
{
    public struct RaceLapIndex : IEquatable<RaceLapIndex>
    {
        public RaceLapIndex(int index, decimal round, decimal roundsToGo, decimal passedLength)
        {
            this.Index = index;
            this.Round = round;
            this.RoundsToGo = roundsToGo;
            this.PassedLength = passedLength;
        }

        public int Index { get; }

        public decimal Round { get; }

        public decimal RoundsToGo { get; }

        public decimal PassedLength { get; }

        public bool Equals(RaceLapIndex other)
        {
            return Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is RaceLapIndex && Equals((RaceLapIndex)obj);
        }

        public override int GetHashCode()
        {
            return Index;
        }

        public static bool operator ==(RaceLapIndex left, RaceLapIndex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RaceLapIndex left, RaceLapIndex right)
        {
            return !left.Equals(right);
        }
    }
}