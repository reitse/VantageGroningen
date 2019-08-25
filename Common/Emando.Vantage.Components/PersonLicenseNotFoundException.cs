using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class PersonLicenseNotFoundException : Exception
    {
        public PersonLicenseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PersonLicenseNotFoundException(string issuerId, string discipline, string key)
            : base(string.Format(Resources.PersonLicenseNotFound, issuerId, discipline, key))
        {
            IssuerId = issuerId;
            Discipline = discipline;
            Key = key;
        }

        public PersonLicenseNotFoundException(string issuerId, string discipline, string key, Exception innerException)
            : base(string.Format(Resources.PersonLicenseNotFound, issuerId, discipline, key), innerException)
        {
            IssuerId = issuerId;
            Discipline = discipline;
            Key = key;
        }

        public string IssuerId { get; }

        public string Discipline { get; }

        public string Key { get; }
    }
}