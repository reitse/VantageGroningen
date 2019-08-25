using System;
using System.Data;
using System.Data.SqlClient;

namespace Emando.Vantage.Components
{
    public class SqlVenueTarget : SqlSyncTargetBase<IVenue>
    {
        public SqlVenueTarget(SqlVenueSource source) : base(source)
        {
        }

        protected override SqlCommand CreateDeleteCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM dbo.Venues " +
                "WHERE [Code] = @Code AND [Discipline] = @Discipline";
            command.Parameters.Add("@Code", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            return command;
        }

        protected override void SetDeleteParameters(SqlCommand command, IVenue item)
        {
            command.Parameters["@Code"].Value = item.Code;
            command.Parameters["@Discipline"].Value = item.Discipline;
        }

        protected override SqlCommand CreateUpdateCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE dbo.Venues " +
                "SET Name = @Name, Address_Line1 = @Address_Line1, Address_Line2 = @Address_Line2, " +
                "Address_StateOrProvince = @Address_StateOrProvince, Address_PostalCode = @Address_PostalCode, " +
                "Address_City = @Address_City, Address_CountryCode = @Address_CountryCode, " +
                "ContinentCode = @ContinentCode " +
                "WHERE [Code] = @Code AND [Discipline] = @Discipline";
            command.Parameters.Add("@Code", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_Line1", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_Line2", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_StateOrProvince", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Address_PostalCode", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Address_City", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_CountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@ContinentCode", SqlDbType.NVarChar, 3);
            return command;
        }

        protected override void SetUpdateParameters(SqlCommand command, IVenue item)
        {
            command.Parameters["@Code"].Value = item.Code;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@Name"].Value = item.Name;
            command.Parameters["@Address_Line1"].Value = (object)item.Address.Line1 ?? DBNull.Value;
            command.Parameters["@Address_Line2"].Value = (object)item.Address.Line2 ?? DBNull.Value;
            command.Parameters["@Address_StateOrProvince"].Value = (object)item.Address.StateOrProvince ?? DBNull.Value;
            command.Parameters["@Address_PostalCode"].Value = (object)item.Address.PostalCode ?? DBNull.Value;
            command.Parameters["@Address_City"].Value = (object)item.Address.City ?? DBNull.Value;
            command.Parameters["@Address_CountryCode"].Value = (object)item.Address.CountryCode ?? DBNull.Value;
            command.Parameters["@ContinentCode"].Value = (object)item.ContinentCode ?? DBNull.Value;
        }

        protected override SqlCommand CreateInsertCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.Venues ([Code], [Discipline], Name, Address_Line1, Address_Line2, " +
                "Address_StateOrProvince, Address_PostalCode, Address_City, Address_CountryCode, ContinentCode) " +
                "VALUES (@Code, @Discipline, @Name, @Address_Line1, @Address_Line2, @Address_StateOrProvince, " +
                "@Address_PostalCode, @Address_City, @Address_CountryCode, @ContinentCode)";
            command.Parameters.Add("@Code", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_Line1", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_Line2", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_StateOrProvince", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Address_PostalCode", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Address_City", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_CountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@ContinentCode", SqlDbType.NVarChar, 3);
            return command;
        }

        protected override void SetInsertParameters(SqlCommand command, IVenue item)
        {
            command.Parameters["@Code"].Value = item.Code;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@Name"].Value = item.Name;
            command.Parameters["@Address_Line1"].Value = (object)item.Address.Line1 ?? DBNull.Value;
            command.Parameters["@Address_Line2"].Value = (object)item.Address.Line2 ?? DBNull.Value;
            command.Parameters["@Address_StateOrProvince"].Value = (object)item.Address.StateOrProvince ?? DBNull.Value;
            command.Parameters["@Address_PostalCode"].Value = (object)item.Address.PostalCode ?? DBNull.Value;
            command.Parameters["@Address_City"].Value = (object)item.Address.City ?? DBNull.Value;
            command.Parameters["@Address_CountryCode"].Value = (object)item.Address.CountryCode ?? DBNull.Value;
            command.Parameters["@ContinentCode"].Value = (object)item.ContinentCode ?? DBNull.Value;
        }
    }
}
