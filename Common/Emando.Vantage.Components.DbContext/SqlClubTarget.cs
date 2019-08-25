using System;
using System.Data;
using System.Data.SqlClient;

namespace Emando.Vantage.Components
{
    public class SqlClubTarget : SqlSyncTargetBase<IClub>
    {
        public SqlClubTarget(SqlClubSource source) : base(source)
        {
        }

        protected override SqlCommand CreateDeleteCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM dbo.Clubs " +
                "WHERE CountryCode = @CountryCode AND Code = @Code";
            command.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@Code", SqlDbType.Int);
            return command;
        }

        protected override void SetDeleteParameters(SqlCommand command, IClub item)
        {
            command.Parameters["@CountryCode"].Value = item.CountryCode;
            command.Parameters["@Code"].Value = item.Code;
        }

        protected override SqlCommand CreateUpdateCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE dbo.Clubs " +
                "SET ShortName = @ShortName, FullName = @FullName " +
                "WHERE CountryCode = @CountryCode AND Code = @Code";
            command.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@Code", SqlDbType.Int);
            command.Parameters.Add("@ShortName", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@FullName", SqlDbType.NVarChar, 100);
            return command;
        }

        protected override void SetUpdateParameters(SqlCommand command, IClub item)
        {
            command.Parameters["@CountryCode"].Value = item.CountryCode;
            command.Parameters["@Code"].Value = item.Code;
            command.Parameters["@ShortName"].Value = (object)item.ShortName ?? DBNull.Value;
            command.Parameters["@FullName"].Value = item.FullName;
        }

        protected override SqlCommand CreateInsertCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.Clubs (CountryCode, Code, ShortName, FullName) " +
                "VALUES (@CountryCode, @Code, @ShortName, @FullName)";
            command.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@Code", SqlDbType.Int);
            command.Parameters.Add("@ShortName", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@FullName", SqlDbType.NVarChar, 100);
            return command;
        }

        protected override void SetInsertParameters(SqlCommand command, IClub item)
        {
            command.Parameters["@CountryCode"].Value = item.CountryCode;
            command.Parameters["@Code"].Value = item.Code;
            command.Parameters["@ShortName"].Value = (object)item.ShortName ?? DBNull.Value;
            command.Parameters["@FullName"].Value = item.FullName;
        }
    }
}