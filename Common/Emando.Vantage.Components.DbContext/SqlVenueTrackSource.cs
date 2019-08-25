using Emando.Vantage.Entities;
using System.Data.SqlClient;

namespace Emando.Vantage.Components
{
    public class SqlVenueTrackSource : SqlSyncSourceBase<IVenueTrack>
    {
        public SqlVenueTrackSource(string connectionString) : base(connectionString)
        {
        }

        protected override SqlCommand CreateSelectCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT [VenueCode], [VenueDiscipline], [Length] " +
                "FROM dbo.VenueTracks";
            return command;
        }

        protected override IVenueTrack Read(SqlDataReader reader)
        {
            return new VenueTrack
            {
                VenueCode = (string)reader["VenueCode"],
                VenueDiscipline = (string)reader["VenueDiscipline"],
                Length = (decimal)reader["Length"]
            };
        }
    }
}
