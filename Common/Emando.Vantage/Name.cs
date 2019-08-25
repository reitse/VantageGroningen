using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Emando.Vantage
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02")]
    public class Name
    {
        public Name()
        {
        }

        public Name(string initials, string firstName, string surname)
        {
            Initials = initials;
            FirstName = firstName;
            Surname = surname;
        }

        public Name(string initials, string firstName, string surnamePrefix, string surname)
        {
            Initials = initials;
            FirstName = firstName;
            SurnamePrefix = surnamePrefix;
            Surname = surname;
        }

        [StringLength(20)]
        [DataMember]
        public string Initials { get; set; }

        [StringLength(100)]
        [DataMember]
        public string FirstName { get; set; }

        [StringLength(20)]
        [DataMember]
        public string SurnamePrefix { get; set; }

        [StringLength(100)]
        [DataMember]
        public string Surname { get; set; }

        public string PrefixedSurname => SurnamePrefix != null || Surname != null ? $"{SurnamePrefix} {Surname}".Trim() : null;

        public override string ToString()
        {
            return FirstName != null || PrefixedSurname != null ? $"{FirstName} {PrefixedSurname}".Trim() : string.Empty;
        }

        public string ToInitialNameString()
        {
            return Initials != null || PrefixedSurname != null ? $"{Initials} {PrefixedSurname}".Trim() : null;
        }

        public bool Equals(Name other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.FirstName, FirstName) && Equals(other.SurnamePrefix, SurnamePrefix) && Equals(other.Surname, Surname);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(Name))
                return false;
            return Equals((Name)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = FirstName?.GetHashCode() ?? 0;
                result = (result * 397) ^ (SurnamePrefix?.GetHashCode() ?? 0);
                result = (result * 397) ^ (Surname?.GetHashCode() ?? 0);
                return result;
            }
        }

        public static bool operator ==(Name left, Name right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Name left, Name right)
        {
            return !Equals(left, right);
        }

        public static Name Parse(string s)
        {
            var parts = s.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return new Name
                {
                    Surname = parts[0]
                };

            if (parts.Length == 2)
                return new Name
                {
                    FirstName = parts[0],
                    Surname = parts[1]
                };

            var name = new Name
            {
                FirstName = parts[0]
            };
            var prefixParts = parts.Skip(1).TakeWhile(p => char.IsLower(p[0])).ToList();
            if (prefixParts.Any())
                name.SurnamePrefix = string.Join(" ", prefixParts);
            name.Surname = string.Join(" ", parts.Skip(prefixParts.Count + 1));
            return name;
        }
    }
}