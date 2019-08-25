using System;
using System.Data.SqlClient;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components
{
    public class SqlPersonLicenseSource : SqlSyncSourceBase<IPersonLicense>
    {
        public SqlPersonLicenseSource(string connectionString) : base(connectionString)
        {
        }

        protected override SqlCommand CreateSelectCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT IssuerId, Discipline, [Key], PersonId, VenueCode, Sponsor, Flags, Season, ValidFrom, ValidTo, Category, LegNumber, Number, " +
                "ClubCountryCode, ClubCode, Transponder1, Transponder2 " +
                "FROM dbo.PersonLicenses";
            return command;
        }

        protected override IPersonLicense Read(SqlDataReader reader)
        {
            return new PersonLicense
            {
                IssuerId = (string)reader[0],
                Discipline = (string)reader[1],
                Key = (string)reader[2],
                PersonId = (Guid)reader[3],
                VenueCode = reader[4] as string,
                Sponsor = reader[5] as string,
                Flags = (PersonLicenseFlags)(int)reader[6],
                Season = (int)reader[7],
                ValidFrom = (DateTime)reader[8],
                ValidTo = (DateTime)reader[9],
                Category = reader[10] as string,
                LegNumber = reader[11] as string,
                Number = reader[12] as int?,
                ClubCountryCode = reader[13] as string,
                ClubCode = reader[14] as int?,
                Transponder1 = reader[15] as string,
                Transponder2 = reader[16] as string
            };
        }
    }
}