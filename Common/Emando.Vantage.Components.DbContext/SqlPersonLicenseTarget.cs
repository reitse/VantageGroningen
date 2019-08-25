using System;
using System.Data;
using System.Data.SqlClient;

namespace Emando.Vantage.Components
{
    public class SqlPersonLicenseTarget : SqlSyncTargetBase<IPersonLicense>
    {
        public SqlPersonLicenseTarget(SqlPersonLicenseSource source) : base(source)
        {
        }

        protected override SqlCommand CreateDeleteCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM dbo.PersonLicenses WHERE IssuerId = @IssuerId AND Discipline = @Discipline AND [Key] = @Key";
            command.Parameters.Add("@IssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Key", SqlDbType.NVarChar, 100);
            return command;
        }

        protected override void SetDeleteParameters(SqlCommand command, IPersonLicense item)
        {
            command.Parameters["@IssuerId"].Value = item.IssuerId;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@Key"].Value = item.Key;
        }

        protected override SqlCommand CreateUpdateCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE dbo.PersonLicenses " +
                "SET PersonId = @PersonId, VenueCode = @VenueCode, Sponsor = @Sponsor, Flags = @Flags, Season = @Season, ValidFrom = @ValidFrom, ValidTo = @ValidTo, " +
                "Category = @Category, LegNumber = @LegNumber, Number = @Number, ClubCountryCode = @ClubCountryCode, ClubCode = @ClubCode, " +
                "Transponder1 = @Transponder1, Transponder2 = @Transponder2 " +
                "WHERE IssuerId = @IssuerId AND Discipline = @Discipline AND [Key] = @Key";
            command.Parameters.Add("@IssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Key", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@PersonId", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Sponsor", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Flags", SqlDbType.Int);
            command.Parameters.Add("@Season", SqlDbType.Int);
            command.Parameters.Add("@ValidFrom", SqlDbType.Date);
            command.Parameters.Add("@ValidTo", SqlDbType.Date);
            command.Parameters.Add("@Category", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@LegNumber", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Number", SqlDbType.Int);
            command.Parameters.Add("@ClubCountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@ClubCode", SqlDbType.Int);
            command.Parameters.Add("@Transponder1", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Transponder2", SqlDbType.NVarChar, 50);
            return command;
        }

        protected override void SetUpdateParameters(SqlCommand command, IPersonLicense item)
        {
            command.Parameters["@IssuerId"].Value = item.IssuerId;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@Key"].Value = item.Key;
            command.Parameters["@PersonId"].Value = item.PersonId;
            command.Parameters["@VenueCode"].Value = (object)item.VenueCode ?? DBNull.Value;
            command.Parameters["@Sponsor"].Value = (object)item.Sponsor ?? DBNull.Value;
            command.Parameters["@Flags"].Value = item.Flags;
            command.Parameters["@Season"].Value = item.Season;
            command.Parameters["@ValidFrom"].Value = item.ValidFrom;
            command.Parameters["@ValidTo"].Value = item.ValidTo;
            command.Parameters["@Category"].Value = (object)item.Category ?? DBNull.Value;
            command.Parameters["@LegNumber"].Value = (object)item.LegNumber ?? DBNull.Value;
            command.Parameters["@Number"].Value = (object)item.Number ?? DBNull.Value;
            command.Parameters["@ClubCountryCode"].Value = (object)item.ClubCountryCode ?? DBNull.Value;
            command.Parameters["@ClubCode"].Value = (object)item.ClubCode ?? DBNull.Value;
            command.Parameters["@Transponder1"].Value = (object)item.Transponder1 ?? DBNull.Value;
            command.Parameters["@Transponder2"].Value = (object)item.Transponder2 ?? DBNull.Value;
        }

        protected override SqlCommand CreateInsertCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.PersonLicenses " +
                "(IssuerId, Discipline, [Key], PersonId, VenueCode, Sponsor, Flags, Season, ValidFrom, ValidTo, Category, LegNumber, Number, ClubCountryCode, ClubCode, " +
                "Transponder1, Transponder2) " +
                "VALUES (@IssuerId, @Discipline, @Key, @PersonId, @VenueCode, @Sponsor, @Flags, @Season, @ValidFrom, @ValidTo, @Category, @LegNumber, @Number, " +
                "@ClubCountryCode, @ClubCode, @Transponder1, @Transponder2)";
            command.Parameters.Add("@IssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Key", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@PersonId", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Sponsor", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Flags", SqlDbType.Int);
            command.Parameters.Add("@Season", SqlDbType.Int);
            command.Parameters.Add("@ValidFrom", SqlDbType.Date);
            command.Parameters.Add("@ValidTo", SqlDbType.Date);
            command.Parameters.Add("@Category", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@LegNumber", SqlDbType.NVarChar, 20);
            command.Parameters.Add("@Number", SqlDbType.Int);
            command.Parameters.Add("@ClubCountryCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@ClubCode", SqlDbType.Int);
            command.Parameters.Add("@Transponder1", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Transponder2", SqlDbType.NVarChar, 50);
            return command;
        }

        protected override void SetInsertParameters(SqlCommand command, IPersonLicense item)
        {
            command.Parameters["@IssuerId"].Value = item.IssuerId;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@Key"].Value = item.Key;
            command.Parameters["@PersonId"].Value = item.PersonId;
            command.Parameters["@VenueCode"].Value = (object)item.VenueCode ?? DBNull.Value;
            command.Parameters["@Sponsor"].Value = (object)item.Sponsor ?? DBNull.Value;
            command.Parameters["@Flags"].Value = item.Flags;
            command.Parameters["@Season"].Value = item.Season;
            command.Parameters["@ValidFrom"].Value = item.ValidFrom;
            command.Parameters["@ValidTo"].Value = item.ValidTo;
            command.Parameters["@Category"].Value = (object)item.Category ?? DBNull.Value;
            command.Parameters["@LegNumber"].Value = (object)item.LegNumber ?? DBNull.Value;
            command.Parameters["@Number"].Value = (object)item.Number ?? DBNull.Value;
            command.Parameters["@ClubCountryCode"].Value = (object)item.ClubCountryCode ?? DBNull.Value;
            command.Parameters["@ClubCode"].Value = (object)item.ClubCode ?? DBNull.Value;
            command.Parameters["@Transponder1"].Value = (object)item.Transponder1 ?? DBNull.Value;
            command.Parameters["@Transponder2"].Value = (object)item.Transponder2 ?? DBNull.Value;
        }
    }
}