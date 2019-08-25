using System;

namespace Emando.Vantage.Workflows.Reporting
{
    public struct DisciplineReportDescription : IEquatable<DisciplineReportDescription>
    {
        public DisciplineReportDescription(string discipline, string key, int order) : this()
        {
            Discipline = discipline;
            Key = key;
            Order = order;
        }

        public string Discipline { get; }

        public string Key { get; }

        public int Order { get; }

        public bool Equals(DisciplineReportDescription other)
        {
            return string.Equals(Discipline, other.Discipline) && string.Equals(Key, other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is DisciplineReportDescription && Equals((DisciplineReportDescription)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Discipline != null ? Discipline.GetHashCode() : 0) * 397) ^ (Key != null ? Key.GetHashCode() : 0);
            }
        }

        public static bool operator ==(DisciplineReportDescription left, DisciplineReportDescription right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DisciplineReportDescription left, DisciplineReportDescription right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"{Discipline}.{Key}";
        }
    }
}