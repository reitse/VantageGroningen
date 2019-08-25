using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2015/07/Competitions")]
    public struct Heat : IEquatable<Heat>, IComparable<Heat>
    {
        public Heat(int round, int number) : this()
        {
            Round = round;
            Number = number;
        }

        [DataMember]
        public int Round { get; set; }

        [DataMember]
        public int Number { get; set; }

        #region IComparable<Heat> Members

        public int CompareTo(Heat other)
        {
            return Round != other.Round ? Round.CompareTo(other.Round) : Number.CompareTo(other.Number);
        }

        #endregion

        #region IEquatable<Heat> Members

        public bool Equals(Heat other)
        {
            return Round == other.Round && Number == other.Number;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Heat && Equals((Heat)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Round * 397) ^ Number;
            }
        }

        public static bool operator ==(Heat left, Heat right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Heat left, Heat right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(Heat left, Heat right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(Heat left, Heat right)
        {
            return left.CompareTo(right) < 0;
        }

        public override string ToString()
        {
            return $"{Round}.{Number}";
        }
    }
}