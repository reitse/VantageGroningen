using System;
using System.Runtime.Serialization;

namespace Emando.Vantage
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02")]
    public struct PresentationSource : IEquatable<PresentationSource>
    {
        public static PresentationSource User = new PresentationSource("", "", "User");

        public PresentationSource(string applianceName, string applianceInstanceName, string how) : this()
        {
            ApplianceName = applianceName;
            ApplianceInstanceName = applianceInstanceName;
            How = how;
        }

        [DataMember]
        public string ApplianceName { get; set; }

        [DataMember]
        public string ApplianceInstanceName { get; set; }

        [DataMember]
        public string How { get; set; }

        #region IEquatable<PresentationSource> Members

        public bool Equals(PresentationSource other)
        {
            return string.Equals(ApplianceName, other.ApplianceName) && string.Equals(ApplianceInstanceName, other.ApplianceInstanceName) && string.Equals(How, other.How);
        }

        #endregion

        public static bool TryParse(string s, out PresentationSource presentationSource)
        {
            presentationSource = default(PresentationSource);

            var parts = s?.Split('/');
            if (parts?.Length != 3)
                return false;

            presentationSource = new PresentationSource(parts[0], parts[1], parts[2]);
            return true;
        }

        public override string ToString()
        {
            return $"{ApplianceName.Replace('/', '-')}/{ApplianceInstanceName.Replace('/', '-')}/{How.Replace('/', '-')}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is PresentationSource && Equals((PresentationSource)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ApplianceName?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (ApplianceInstanceName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (How?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public static bool operator ==(PresentationSource left, PresentationSource right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PresentationSource left, PresentationSource right)
        {
            return !left.Equals(right);
        }
    }
}