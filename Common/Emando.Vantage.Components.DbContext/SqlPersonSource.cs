using System;
using System.Data.SqlClient;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components
{
    public class SqlPersonSource : SqlSyncSourceBase<IPerson>
    {
        public SqlPersonSource(string connectionString) : base(connectionString)
        {
        }

        protected override SqlCommand CreateSelectCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name_Initials, Name_FirstName, Name_SurnamePrefix, Name_Surname, Email, Phone, Address_Line1, Address_Line2, " +
                "Address_StateOrProvince, Address_PostalCode, Address_City, Address_CountryCode, Gender, NationalityCode, BirthDate, Iban " +
                "FROM dbo.People";
            return command;
        }

        protected override IPerson Read(SqlDataReader reader)
        {
            return new Person
            {
                Id = (Guid)reader[0],
                Name = new Name(reader[1] as string, reader[2] as string, reader[3] as string, reader[4] as string),
                Email = reader[5] as string,
                Phone = reader[6] as string,
                Address = new Address
                {
                    Line1 = reader[7] as string,
                    Line2 = reader[8] as string,
                    StateOrProvince = reader[9] as string,
                    PostalCode = reader[10] as string,
                    City = reader[11] as string,
                    CountryCode = reader[12] as string
                },
                Gender = (Gender)(int)reader[13],
                NationalityCode = reader[14] as string,
                BirthDate = (DateTime)reader[15],
                Iban = reader[16] as string
            };
        }
    }
}