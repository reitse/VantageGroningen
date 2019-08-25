using System;

namespace Emando.Vantage
{
    public struct CompetitionClassLocation : IEquatable<CompetitionClassLocation>
    {
        public int CompetitionClass { get; set; }

        public string Value { get; set; }

        public CompetitionClassLocation(int competitionClass, string value)
        {
            CompetitionClass = competitionClass;
            Value = value;
        }

        public bool Equals(CompetitionClassLocation other)
        {
            return CompetitionClass == other.CompetitionClass && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is CompetitionClassLocation && Equals((CompetitionClassLocation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CompetitionClass * 397) ^ (Value?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(CompetitionClassLocation left, CompetitionClassLocation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CompetitionClassLocation left, CompetitionClassLocation right)
        {
            return !left.Equals(right);
        }
    }
}