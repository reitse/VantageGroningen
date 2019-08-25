using System.Data.SqlClient;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components
{
    public class SqlClubSource : SqlSyncSourceBase<IClub>
    {
        public SqlClubSource(string connectionString) : base(connectionString)
        {
        }

        protected override SqlCommand CreateSelectCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT CountryCode, Code, ShortName, FullName " +
                "FROM dbo.Clubs";
            return command;
        }

        protected override IClub Read(SqlDataReader reader)
        {
            return new Club
            {
                CountryCode = (string)reader["CountryCode"],
                Code = (int)reader["Code"],
                ShortName = reader["ShortName"] as string,
                FullName = (string)reader["FullName"]
            };
        }
    }
}