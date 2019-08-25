using System;
using System.Data;
using System.Data.SqlClient;

namespace Emando.Vantage.Components
{
    public class SqlPersonCategoryTarget : SqlSyncTargetBase<IPersonCategory>
    {
        public SqlPersonCategoryTarget(SqlPersonCategorySource source) : base(source)
        {
        }

        protected override SqlCommand CreateDeleteCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM dbo.PersonCategories " +
                "WHERE LicenseIssuerId = @LicenseIssuerId AND Discipline = @Discipline AND Code = @Code";
            command.Parameters.Add("@LicenseIssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Code", SqlDbType.NVarChar, 20);
            return command;
        }

        protected override void SetDeleteParameters(SqlCommand command, IPersonCategory item)
        {
            command.Parameters["@LicenseIssuerId"].Value = item.LicenseIssuerId;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@Code"].Value = item.Code;
        }

        protected override SqlCommand CreateUpdateCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE dbo.PersonCategories " +
                "SET FromAge = @FromAge, ToAge = @ToAge, Gender = @Gender, Name = @Name, Flags = @Flags " +
                "WHERE LicenseIssuerId = @LicenseIssuerId AND Discipline = @Discipline AND Code = @Code";
            command.Parameters.Add("@FromAge", SqlDbType.Int);
            command.Parameters.Add("@ToAge", SqlDbType.Int);
            command.Parameters.Add("@Gender", SqlDbType.Int);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Flags", SqlDbType.Int);
            command.Parameters.Add("@LicenseIssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Code", SqlDbType.NVarChar, 20);
            return command;
        }

        protected override void SetUpdateParameters(SqlCommand command, IPersonCategory item)
        {
            command.Parameters["@FromAge"].Value = item.FromAge;
            command.Parameters["@ToAge"].Value = item.ToAge;
            command.Parameters["@Gender"].Value = item.Gender;
            command.Parameters["@Name"].Value = item.Name;
            command.Parameters["@Flags"].Value = item.Flags;
            command.Parameters["@LicenseIssuerId"].Value = item.LicenseIssuerId;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@Code"].Value = item.Code;
        }

        protected override SqlCommand CreateInsertCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.PersonCategories (LicenseIssuerId, Discipline, Code, FromAge, ToAge, Gender, Name, Flags) " +
                "VALUES (@LicenseIssuerId, @Discipline, @Code, @FromAge, @ToAge, @Gender, @Name, @Flags)";
            command.Parameters.Add("@LicenseIssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Code", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@FromAge", SqlDbType.Int);
            command.Parameters.Add("@ToAge", SqlDbType.Int);
            command.Parameters.Add("@Gender", SqlDbType.Int);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Flags", SqlDbType.Int);
            return command;
        }

        protected override void SetInsertParameters(SqlCommand command, IPersonCategory item)
        {
            command.Parameters["@LicenseIssuerId"].Value = item.LicenseIssuerId;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@Code"].Value = item.Code;
            command.Parameters["@FromAge"].Value = item.FromAge;
            command.Parameters["@ToAge"].Value = item.ToAge;
            command.Parameters["@Gender"].Value = item.Gender;
            command.Parameters["@Name"].Value = item.Name;
            command.Parameters["@Flags"].Value = item.Flags;
        }
    }
}