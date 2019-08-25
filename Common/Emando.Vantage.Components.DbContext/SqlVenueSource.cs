using Emando.Vantage.Entities;
using System.Data.SqlClient;

namespace Emando.Vantage.Components
{
    public class SqlVenueSource : SqlSyncSourceBase<IVenue>
    {
        public SqlVenueSource(string connectionString) : base(connectionString)
        {
        }

        protected override SqlCommand CreateSelectCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT [Code], [Discipline], [Name], [Address_Line1], [Address_Line2], [Address_StateOrProvince], " +
                "[Address_PostalCode], [Address_City], [Address_CountryCode], [ContinentCode] " +
                "FROM dbo.Venues";
            return command;
        }

        protected override IVenue Read(SqlDataReader reader)
        {
            return new Venue
            {
                Code = (string)reader["Code"],
                Discipline = (string)reader["Discipline"],
                Name = (string)reader["Name"],
                Address = new Address
                {
                    Line1 = reader["Address_Line1"] as string,
                    Line2 = reader["Address_Line2"] as string,
                    StateOrProvince = reader["Address_StateOrProvince"] as string,
                    PostalCode = reader["Address_PostalCode"] as string,
                    City = reader["Address_City"] as string,
                    CountryCode = reader["Address_CountryCode"] as string,
                },
                ContinentCode = reader["ContinentCode"] as string,
            };
        }
    }
}
