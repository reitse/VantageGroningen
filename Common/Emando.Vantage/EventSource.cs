using System;
using System.Runtime.Serialization;

namespace Emando.Vantage
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02")]
    public struct EventSource : IEquatable<EventSource>
    {
        public EventSource(string applianceName, string applianceInstanceName, string how, long where) : this()
        {
            ApplianceName = applianceName;
            ApplianceInstanceName = applianceInstanceName;
            How = how;
            Where = @where;
        }

        [DataMember]
        public string ApplianceName { get; set; }

        [DataMember]
        public string ApplianceInstanceName { get; set; }

        [DataMember]
        public string How { get; set; }

        [DataMember]
        public long Where { get; set; }

        #region IEquatable<EventSource> Members

        public bool Equals(EventSource other)
        {
            return string.Equals(ApplianceName, other.ApplianceName) && string.Equals(ApplianceInstanceName, other.ApplianceInstanceName) && string.Equals(How, other.How)
                && Where == other.Where;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is EventSource && Equals((EventSource)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ApplianceName?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (ApplianceInstanceName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (How?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Where.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(EventSource left, EventSource right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EventSource left, EventSource right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"{ApplianceName}/{ApplianceInstanceName}/{How}/{Where}";
        }
    }
}