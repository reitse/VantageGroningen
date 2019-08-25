using System.Data.SqlClient;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public class SqlPersonTimeSource : SqlSyncSourceBase<IPersonLicenseTime>
    {
        public SqlPersonTimeSource(string connectionString, string source) : base(connectionString)
        {
            Source = source;
        }

        public override string Source { get; }

        protected override SqlCommand CreateSelectCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT LicenseIssuerId, LicenseDiscipline, LicenseKey, VenueCode, Discipline, DistanceDiscipline, Distance, [Date], [Time], " +
                "[NationalityCode], [Source] " +
                "FROM Competitions.PersonTimes";
            return command;
        }

        protected override IPersonLicenseTime Read(SqlDataReader reader)
        {
            return new PersonTime
            {
                LicenseIssuerId = reader.GetString(0),
                LicenseDiscipline = reader.GetString(1),
                LicenseKey = reader.GetString(2),
                VenueCode = reader.GetString(3),
                Discipline = reader.GetString(4),
                DistanceDiscipline = reader.GetString(5),
                Distance = reader.GetInt32(6),
                Date = reader.GetDateTime(7),
                Time = reader.GetTimeSpan(8),
                NationalityCode = !reader.IsDBNull(9) ? reader.GetString(9) : null,
                Source = reader.GetString(10)
            };
        }
    }
}