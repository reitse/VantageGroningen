using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2015/07/Competitions")]
    public class CalculatedLap : ICalculatedLap, IEquatable<CalculatedLap>
    {
        public CalculatedLap(TimeSpan time, int index, TimeSpan lapTime, decimal rounds, decimal roundsToGo, int passedLength, int? ranking = null)
        {
            Time = time;
            Index = index;
            LapTime = lapTime;
            Rounds = rounds;
            RoundsToGo = roundsToGo;
            PassedLength = passedLength;
            Ranking = ranking;
        }

        #region ICalculatedLap Members

        [DataMember]
        public TimeSpan Time { get; private set; }

        [DataMember]
        public int Index { get; private set; }

        [DataMember]
        public TimeSpan LapTime { get; private set; }

        [DataMember]
        public decimal Rounds { get; private set; }

        [DataMember]
        public decimal RoundsToGo { get; private set; }

        [DataMember]
        public int PassedLength { get; private set; }

        [DataMember]
        public int? Ranking { get; private set; }

        #endregion

        #region IEquatable<CalculatedLap> Members

        public bool Equals(CalculatedLap other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Time.Equals(other.Time) && Index == other.Index && LapTime.Equals(other.LapTime) && Rounds == other.Rounds && RoundsToGo == other.RoundsToGo
                && PassedLength == other.PassedLength && Ranking == other.Ranking;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((CalculatedLap)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Time.GetHashCode();
                hashCode = (hashCode * 397) ^ Index;
                hashCode = (hashCode * 397) ^ LapTime.GetHashCode();
                hashCode = (hashCode * 397) ^ Rounds.GetHashCode();
                hashCode = (hashCode * 397) ^ RoundsToGo.GetHashCode();
                hashCode = (hashCode * 397) ^ PassedLength;
                hashCode = (hashCode * 397) ^ Ranking.GetHashCode();
                return hashCode;
            }
        }
    }
}