using System.Data.SqlClient;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public class SqlRecordTimeSource : SqlSyncSourceBase<IRecordTime>
    {
        public SqlRecordTimeSource(string connectionString, string source) : base(connectionString)
        {
            Source = source;
        }

        public override string Source { get; }

        protected override SqlCommand CreateSelectCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT LicenseIssuerId, [Type], Gender, FromAge, ToAge, VenueCode, Discipline, DistanceDiscipline, Distance, Name, [Date], [Time], NationalityCode " +
                "FROM Competitions.RecordTimes";
            return command;
        }

        protected override IRecordTime Read(SqlDataReader reader)
        {
            return new RecordTime
            {
                LicenseIssuerId = reader.GetString(0),
                Type = (RecordType)reader.GetInt32(1),
                Gender = (Gender)reader.GetInt32(2),
                FromAge = reader.GetInt32(3),
                ToAge = reader.GetInt32(4),
                VenueCode = reader.GetString(5),
                Discipline = reader.GetString(6),
                DistanceDiscipline = reader.GetString(7),
                Distance = reader.GetInt32(8),
                Name = !reader.IsDBNull(9) ? reader.GetString(9) : null,
                Date = reader.GetDateTime(10),
                Time = reader.GetTimeSpan(11),
                NationalityCode = !reader.IsDBNull(12) ? reader.GetString(12) : null
            };
        }
    }
}