using System;
using System.Data;
using System.Data.SqlClient;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public class SqlPersonTimeTarget : SqlSyncTargetBase<IPersonLicenseTime>
    {
        private readonly string source;

        public SqlPersonTimeTarget(SqlPersonTimeSource source) : base(source)
        {
            this.source = source.Source;
        }

        public override bool CanDelete(IPersonLicenseTime item)
        {
            return item.Source == source;
        }

        protected override SqlCommand CreateDeleteCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM [Competitions].PersonTimes " +
                "WHERE LicenseIssuerId = @LicenseIssuerId " +
                "AND LicenseDiscipline = @LicenseDiscipline " +
                "AND LicenseKey = @LicenseKey " +
                "AND VenueCode = @VenueCode " +
                "AND Discipline = @Discipline " +
                "AND DistanceDiscipline = @DistanceDiscipline " +
                "AND Distance = @Distance " +
                "AND [Date] = @Date " +
                "AND [Time] = @Time " +
                "AND Source = @Source";
            command.Parameters.Add("@LicenseIssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@LicenseDiscipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@LicenseKey", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@DistanceDiscipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Distance", SqlDbType.Int, 4);
            command.Parameters.Add("@Date", SqlDbType.Date);
            command.Parameters.Add("@Time", SqlDbType.Time, 7);
            command.Parameters.Add("@Source", SqlDbType.NVarChar, 50).Value = source;
            return command;
        }

        protected override void SetDeleteParameters(SqlCommand command, IPersonLicenseTime item)
        {
            command.Parameters["@LicenseIssuerId"].Value = item.LicenseIssuerId;
            command.Parameters["@LicenseDiscipline"].Value = item.LicenseDiscipline;
            command.Parameters["@LicenseKey"].Value = item.LicenseKey;
            command.Parameters["@VenueCode"].Value = item.VenueCode;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@DistanceDiscipline"].Value = item.DistanceDiscipline;
            command.Parameters["@Distance"].Value = item.Distance;
            command.Parameters["@Date"].Value = item.Date;
            command.Parameters["@Time"].Value = item.Time;
        }

        public override bool CanUpdate(IPersonLicenseTime time)
        {
            return false;
        }

        protected override SqlCommand CreateUpdateCommand(SqlConnection connection)
        {
            return connection.CreateCommand();
        }

        protected override void SetUpdateParameters(SqlCommand command, IPersonLicenseTime item)
        {
            throw new NotSupportedException();
        }

        protected override SqlCommand CreateInsertCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO [Competitions].PersonTimes " +
                "(LicenseIssuerId, LicenseDiscipline, LicenseKey, VenueCode, Discipline, DistanceDiscipline, Distance, Date, Time, NationalityCode, Source) " +
                "VALUES (@LicenseIssuerId, @LicenseDiscipline, @LicenseKey, @VenueCode, @Discipline, @DistanceDiscipline, @Distance, @Date, @Time, " +
                "@NationalityCode, @Source)";
            command.Parameters.Add("@LicenseIssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@LicenseDiscipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@LicenseKey", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@DistanceDiscipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Distance", SqlDbType.Int, 4);
            command.Parameters.Add("@Date", SqlDbType.Date);
            command.Parameters.Add("@Time", SqlDbType.Time, 7);
            command.Parameters.Add("@NationalityCode", SqlDbType.NVarChar, 3);
            command.Parameters.Add("@Source", SqlDbType.NVarChar, 50).Value = source;
            return command;
        }

        protected override void SetInsertParameters(SqlCommand command, IPersonLicenseTime item)
        {
            command.Parameters["@LicenseIssuerId"].Value = item.LicenseIssuerId;
            command.Parameters["@LicenseDiscipline"].Value = item.LicenseDiscipline;
            command.Parameters["@LicenseKey"].Value = item.LicenseKey;
            command.Parameters["@VenueCode"].Value = item.VenueCode;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@DistanceDiscipline"].Value = item.DistanceDiscipline;
            command.Parameters["@Distance"].Value = item.Distance;
            command.Parameters["@Date"].Value = item.Date;
            command.Parameters["@Time"].Value = item.Time;
            command.Parameters["@NationalityCode"].Value = item.NationalityCode;
        }
    }
}