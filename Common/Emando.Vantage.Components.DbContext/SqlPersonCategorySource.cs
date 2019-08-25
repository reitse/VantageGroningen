using System.Data.SqlClient;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components
{
    public class SqlPersonCategorySource : SqlSyncSourceBase<IPersonCategory>
    {
        public SqlPersonCategorySource(string connectionString) : base(connectionString)
        {
        }

        protected override SqlCommand CreateSelectCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT LicenseIssuerId, Discipline, Code, FromAge, ToAge, Gender, Name, Flags " +
                "FROM dbo.PersonCategories";
            return command;
        }

        protected override IPersonCategory Read(SqlDataReader reader)
        {
            return new PersonCategory
            {
                LicenseIssuerId = (string)reader["LicenseIssuerId"],
                Discipline = (string)reader["Discipline"],
                Code = (string)reader["Code"],
                FromAge = (int)reader["FromAge"],
                ToAge = (int)reader["ToAge"],
                Gender = (Gender)(int)reader["Gender"],
                Name = (string)reader["Name"],
                Flags = (PersonCategoryFlags)(int)reader["Flags"]
            };
        }
    }
}