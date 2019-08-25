using System;

namespace Emando.Vantage
{
    public struct CompetitionRight : ICompetitionRight, IEquatable<CompetitionRight>
    {
        public CompetitionRight(string licenseIssuerId, string discipline, int competitionClass, string value, string roleName)
        {
            LicenseIssuerId = licenseIssuerId;
            Discipline = discipline;
            CompetitionClass = competitionClass;
            Value = value;
            RoleName = roleName;
        }

        public string LicenseIssuerId { get; }

        public string Discipline { get; }

        public int CompetitionClass { get; }

        public string Value { get; }

        public string RoleName { get; }

        public bool Equals(CompetitionRight other)
        {
            return string.Equals(LicenseIssuerId, other.LicenseIssuerId) && string.Equals(Discipline, other.Discipline) && CompetitionClass == other.CompetitionClass
                && string.Equals(Value, other.Value) && string.Equals(RoleName, other.RoleName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is CompetitionRight && Equals((CompetitionRight)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = LicenseIssuerId?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Discipline?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ CompetitionClass;
                hashCode = (hashCode * 397) ^ (Value?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (RoleName?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public static bool operator ==(CompetitionRight left, CompetitionRight right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CompetitionRight left, CompetitionRight right)
        {
            return !left.Equals(right);
        }
    }
}