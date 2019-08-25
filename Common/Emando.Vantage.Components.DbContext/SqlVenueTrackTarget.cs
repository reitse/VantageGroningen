using System;
using System.Data;
using System.Data.SqlClient;

namespace Emando.Vantage.Components
{
    public class SqlVenueTrackTarget : SqlSyncTargetBase<IVenueTrack>
    {
        public SqlVenueTrackTarget(SqlVenueTrackSource source) : base(source)
        {
        }

        public override bool CanUpdate(IVenueTrack item)
        {
            return false;
        }

        protected override SqlCommand CreateDeleteCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM dbo.VenueTracks " +
                "WHERE [VenueCode] = @VenueCode AND [VenueDiscipline] = @VenueDiscipline AND [Length] = @Length";
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@VenueDiscipline", SqlDbType.NVarChar, 100);
            var length = command.Parameters.Add("@Length", SqlDbType.Decimal);
            length.Precision = 18;
            length.Scale = 3;
            return command;
        }

        protected override void SetDeleteParameters(SqlCommand command, IVenueTrack item)
        {
            command.Parameters["@VenueCode"].Value = item.VenueCode;
            command.Parameters["@VenueDiscipline"].Value = item.VenueDiscipline;
            command.Parameters["@Length"].Value = item.Length;
        }

        protected override SqlCommand CreateInsertCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.VenueTracks ([VenueCode], [VenueDiscipline], [Length]) " +
                "VALUES (@VenueCode, @VenueDiscipline, @Length)";
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@VenueDiscipline", SqlDbType.NVarChar, 100);
            var length = command.Parameters.Add("@Length", SqlDbType.Decimal);
            length.Precision = 18;
            length.Scale = 3;
            return command;
        }

        protected override void SetInsertParameters(SqlCommand command, IVenueTrack item)
        {
            command.Parameters["@VenueCode"].Value = item.VenueCode;
            command.Parameters["@VenueDiscipline"].Value = item.VenueDiscipline;
            command.Parameters["@Length"].Value = item.Length;
        }

        protected override SqlCommand CreateUpdateCommand(SqlConnection connection)
        {
            return new SqlCommand();
        }

        protected override void SetUpdateParameters(SqlCommand command, IVenueTrack item)
        {
            throw new NotSupportedException();
        }
    }
}
