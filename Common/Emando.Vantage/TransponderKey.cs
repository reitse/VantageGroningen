using System;
using System.Runtime.Serialization;

namespace Emando.Vantage
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02")]
    public struct TransponderKey : IEquatable<TransponderKey>
    {
        public TransponderKey(string type, long code) : this()
        {
            Type = type;
            Code = code;
        }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public long Code { get; set; }

        public bool Equals(TransponderKey other)
        {
            return string.Equals(Type, other.Type) && Code == other.Code;
        }

        public override string ToString()
        {
            return $"{Type}/{Code}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is TransponderKey && Equals((TransponderKey)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Type?.GetHashCode() ?? 0) * 397) ^ Code.GetHashCode();
            }
        }

        public static bool operator ==(TransponderKey left, TransponderKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TransponderKey left, TransponderKey right)
        {
            return !left.Equals(right);
        }
    }
}