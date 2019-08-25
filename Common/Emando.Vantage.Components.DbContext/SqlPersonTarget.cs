using System;
using System.Data;
using System.Data.SqlClient;

namespace Emando.Vantage.Components
{
    public class SqlPersonTarget : SqlSyncTargetBase<IPerson>
    {
        public SqlPersonTarget(SqlPersonSource source) : base(source)
        {
        }

        protected override SqlCommand CreateDeleteCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM dbo.People WHERE Id = @Id";
            command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
            return command;
        }

        protected override void SetDeleteParameters(SqlCommand command, IPerson item)
        {
            command.Parameters["@Id"].Value = item.Id;
        }

        protected override SqlCommand CreateUpdateCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE dbo.People " +
                "SET Name_Initials = @Name_Initials, Name_FirstName = @Name_FirstName, Name_SurnamePrefix = @Name_SurnamePrefix, Name_Surname = @Name_Surname, " +
                "Email = @Email, Phone = @Phone, Address_Line1 = @Address_Line1, Address_Line2 = @Address_Line2, Address_StateOrProvince = @Address_StateOrProvince, " +
                "Address_PostalCode = @Address_PostalCode, Address_City = @Address_City, Gender = @Gender, NationalityCode = @NationalityCode, BirthDate = @BirthDate, " +
                "Iban = @Iban " +
                "WHERE Id = @Id";
            command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@Name_Initials", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Name_FirstName", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Name_SurnamePrefix", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Name_Surname", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Address_Line1", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_Line2", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_StateOrProvince", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Address_PostalCode", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Address_City", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_CountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@Gender", SqlDbType.Int);
            command.Parameters.Add("@NationalityCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@BirthDate", SqlDbType.Date);
            command.Parameters.Add("@Iban", SqlDbType.NVarChar, 34);
            return command;
        }

        protected override void SetUpdateParameters(SqlCommand command, IPerson item)
        {
            command.Parameters["@Id"].Value = item.Id;
            command.Parameters["@Name_Initials"].Value = (object)item.Name.Initials ?? DBNull.Value;
            command.Parameters["@Name_FirstName"].Value = (object)item.Name.FirstName ?? DBNull.Value;
            command.Parameters["@Name_SurnamePrefix"].Value = (object)item.Name.SurnamePrefix ?? DBNull.Value;
            command.Parameters["@Name_Surname"].Value = (object)item.Name.Surname ?? DBNull.Value;
            command.Parameters["@Email"].Value = (object)item.Email ?? DBNull.Value;
            command.Parameters["@Phone"].Value = (object)item.Phone ?? DBNull.Value;
            command.Parameters["@Address_Line1"].Value = (object)item.Address.Line1 ?? DBNull.Value;
            command.Parameters["@Address_Line2"].Value = (object)item.Address.Line2 ?? DBNull.Value;
            command.Parameters["@Address_StateOrProvince"].Value = (object)item.Address.StateOrProvince ?? DBNull.Value;
            command.Parameters["@Address_PostalCode"].Value = (object)item.Address.PostalCode ?? DBNull.Value;
            command.Parameters["@Address_City"].Value = (object)item.Address.City ?? DBNull.Value;
            command.Parameters["@Address_CountryCode"].Value = (object)item.Address.CountryCode ?? DBNull.Value;
            command.Parameters["@Gender"].Value = item.Gender;
            command.Parameters["@NationalityCode"].Value = (object)item.NationalityCode ?? DBNull.Value;
            command.Parameters["@BirthDate"].Value = item.BirthDate;
            command.Parameters["@Iban"].Value = (object)item.Iban ?? DBNull.Value;
        }

        protected override SqlCommand CreateInsertCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.People " +
                "(Id, Name_Initials, Name_FirstName, Name_SurnamePrefix, Name_Surname, Email, Phone, Address_Line1, Address_Line2, Address_StateOrProvince, " +
                "Address_PostalCode, Address_City, Address_CountryCode, Gender, NationalityCode, BirthDate, Iban) " +
                "VALUES (@Id, @Name_Initials, @Name_FirstName, @Name_SurnamePrefix, @Name_Surname, @Email, @Phone, @Address_Line1, @Address_Line2, " +
                "@Address_StateOrProvince, @Address_PostalCode, @Address_City, @Address_CountryCode, @Gender, @NationalityCode, @BirthDate, @Iban)";
            command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@Name_Initials", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Name_FirstName", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Name_SurnamePrefix", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Name_Surname", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Address_Line1", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_Line2", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_StateOrProvince", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Address_PostalCode", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Address_City", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Address_CountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@Gender", SqlDbType.Int);
            command.Parameters.Add("@NationalityCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@BirthDate", SqlDbType.Date);
            command.Parameters.Add("@Iban", SqlDbType.NVarChar, 34);
            return command;
        }

        protected override void SetInsertParameters(SqlCommand command, IPerson item)
        {
            command.Parameters["@Id"].Value = item.Id;
            command.Parameters["@Name_Initials"].Value = (object)item.Name.Initials ?? DBNull.Value;
            command.Parameters["@Name_FirstName"].Value = (object)item.Name.FirstName ?? DBNull.Value;
            command.Parameters["@Name_SurnamePrefix"].Value = (object)item.Name.SurnamePrefix ?? DBNull.Value;
            command.Parameters["@Name_Surname"].Value = (object)item.Name.Surname ?? DBNull.Value;
            command.Parameters["@Email"].Value = (object)item.Email ?? DBNull.Value;
            command.Parameters["@Phone"].Value = (object)item.Phone ?? DBNull.Value;
            command.Parameters["@Address_Line1"].Value = (object)item.Address.Line1 ?? DBNull.Value;
            command.Parameters["@Address_Line2"].Value = (object)item.Address.Line2 ?? DBNull.Value;
            command.Parameters["@Address_StateOrProvince"].Value = (object)item.Address.StateOrProvince ?? DBNull.Value;
            command.Parameters["@Address_PostalCode"].Value = (object)item.Address.PostalCode ?? DBNull.Value;
            command.Parameters["@Address_City"].Value = (object)item.Address.City ?? DBNull.Value;
            command.Parameters["@Address_CountryCode"].Value = (object)item.Address.CountryCode ?? DBNull.Value;
            command.Parameters["@Gender"].Value = item.Gender;
            command.Parameters["@NationalityCode"].Value = (object)item.NationalityCode ?? DBNull.Value;
            command.Parameters["@BirthDate"].Value = item.BirthDate;
            command.Parameters["@Iban"].Value = (object)item.Iban ?? DBNull.Value;
        }
    }
}