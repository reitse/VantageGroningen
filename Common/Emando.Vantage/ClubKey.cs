using System;

namespace Emando.Vantage
{
    public struct ClubKey : IEquatable<ClubKey>
    {
        public string CountryCode { get; }

        public int Code { get; }

        public ClubKey(string countryCode, int code)
        {
            CountryCode = countryCode;
            Code = code;
        }

        public override string ToString()
        {
            return $"{CountryCode}/{Code}";
        }

        public bool Equals(ClubKey other)
        {
            return string.Equals(CountryCode, other.CountryCode) && int.Equals(Code, other.Code);
        }

        public static ClubKey Parse(string s)
        {
            var parts = s.Split('.');
            return new ClubKey(parts[0], int.Parse(parts[1]));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is ClubKey && Equals((ClubKey)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((CountryCode?.GetHashCode() ?? 0) * 397) ^ Code.GetHashCode();
            }
        }

        public static bool operator ==(ClubKey left, ClubKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ClubKey left, ClubKey right)
        {
            return !left.Equals(right);
        }
    }
}