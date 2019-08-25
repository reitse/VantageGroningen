using System.Data;
using System.Data.SqlClient;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public class SqlRecordTimeTarget : SqlSyncTargetBase<IRecordTime>
    {
        public SqlRecordTimeTarget(SqlRecordTimeSource source) : base(source)
        {
        }

        public override bool CanDelete(IRecordTime item)
        {
            return true;
        }

        protected override SqlCommand CreateDeleteCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM [Competitions].RecordTimes " +
                "WHERE LicenseIssuerId = @LicenseIssuerId " +
                "AND [Type] = @Type " +
                "AND [Gender] = @Gender " +
                "AND FromAge = @FromAge " +
                "AND ToAge = @ToAge " +
                "AND VenueCode = @VenueCode " +
                "AND Discipline = @Discipline " +
                "AND DistanceDiscipline = @DistanceDiscipline " +
                "AND Distance = @Distance";
            command.Parameters.Add("@LicenseIssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Type", SqlDbType.Int, 4);
            command.Parameters.Add("@Gender", SqlDbType.Int, 4);
            command.Parameters.Add("@FromAge", SqlDbType.Int, 4);
            command.Parameters.Add("@ToAge", SqlDbType.Int, 4);
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@DistanceDiscipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Distance", SqlDbType.Int, 4);
            return command;
        }

        protected override void SetDeleteParameters(SqlCommand command, IRecordTime item)
        {
            command.Parameters["@LicenseIssuerId"].Value = item.LicenseIssuerId;
            command.Parameters["@Type"].Value = item.Type;
            command.Parameters["@Gender"].Value = item.Gender;
            command.Parameters["@FromAge"].Value = item.FromAge;
            command.Parameters["@ToAge"].Value = item.ToAge;
            command.Parameters["@VenueCode"].Value = item.VenueCode;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@DistanceDiscipline"].Value = item.DistanceDiscipline;
            command.Parameters["@Distance"].Value = item.Distance;
        }

        public override bool CanUpdate(IRecordTime time)
        {
            return true;
        }

        protected override SqlCommand CreateUpdateCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE [Competitions].RecordTimes " +
                "SET Name = @Name, Date = @Date, Time = @Time, NationalityCode = @NationalityCode " +
                "WHERE LicenseIssuerId = @LicenseIssuerId " +
                "AND [Type] = @Type " +
                "AND [Gender] = @Gender " +
                "AND FromAge = @FromAge " +
                "AND ToAge = @ToAge " +
                "AND VenueCode = @VenueCode " +
                "AND Discipline = @Discipline " +
                "AND DistanceDiscipline = @DistanceDiscipline " +
                "AND Distance = @Distance";
            command.Parameters.Add("@LicenseIssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Type", SqlDbType.Int, 4);
            command.Parameters.Add("@Gender", SqlDbType.Int, 4);
            command.Parameters.Add("@FromAge", SqlDbType.Int, 4);
            command.Parameters.Add("@ToAge", SqlDbType.Int, 4);
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@DistanceDiscipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Distance", SqlDbType.Int, 4);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Date", SqlDbType.Date);
            command.Parameters.Add("@Time", SqlDbType.Time, 7);
            command.Parameters.Add("@NationalityCode", SqlDbType.NVarChar, 3);
            return command;
        }

        protected override void SetUpdateParameters(SqlCommand command, IRecordTime item)
        {
            command.Parameters["@LicenseIssuerId"].Value = item.LicenseIssuerId;
            command.Parameters["@Type"].Value = item.Type;
            command.Parameters["@Gender"].Value = item.Gender;
            command.Parameters["@FromAge"].Value = item.FromAge;
            command.Parameters["@ToAge"].Value = item.ToAge;
            command.Parameters["@VenueCode"].Value = item.VenueCode;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@DistanceDiscipline"].Value = item.DistanceDiscipline;
            command.Parameters["@Distance"].Value = item.Distance;
            command.Parameters["@Name"].Value = item.Name;
            command.Parameters["@Date"].Value = item.Date;
            command.Parameters["@Time"].Value = item.Time;
            command.Parameters["@NationalityCode"].Value = item.NationalityCode;
        }

        protected override SqlCommand CreateInsertCommand(SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO [Competitions].RecordTimes " +
                "(LicenseIssuerId, [Type], Gender, FromAge, ToAge, VenueCode, Discipline, DistanceDiscipline, Distance, Name, Date, Time, NationalityCode) " +
                "VALUES (@LicenseIssuerId, @Type, @Gender, @FromAge, @ToAge, @VenueCode, @Discipline, @DistanceDiscipline, @Distance, @Name, @Date, @Time, " +
                "@NationalityCode)";
            command.Parameters.Add("@LicenseIssuerId", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Type", SqlDbType.Int, 4);
            command.Parameters.Add("@Gender", SqlDbType.Int, 4);
            command.Parameters.Add("@FromAge", SqlDbType.Int, 4);
            command.Parameters.Add("@ToAge", SqlDbType.Int, 4);
            command.Parameters.Add("@VenueCode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Discipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@DistanceDiscipline", SqlDbType.NVarChar, 100);
            command.Parameters.Add("@Distance", SqlDbType.Int, 4);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@Date", SqlDbType.Date);
            command.Parameters.Add("@Time", SqlDbType.Time, 7);
            command.Parameters.Add("@NationalityCode", SqlDbType.NVarChar, 3);
            return command;
        }

        protected override void SetInsertParameters(SqlCommand command, IRecordTime item)
        {
            command.Parameters["@LicenseIssuerId"].Value = item.LicenseIssuerId;
            command.Parameters["@Type"].Value = item.Type;
            command.Parameters["@Gender"].Value = item.Gender;
            command.Parameters["@FromAge"].Value = item.FromAge;
            command.Parameters["@ToAge"].Value = item.ToAge;
            command.Parameters["@VenueCode"].Value = item.VenueCode;
            command.Parameters["@Discipline"].Value = item.Discipline;
            command.Parameters["@DistanceDiscipline"].Value = item.DistanceDiscipline;
            command.Parameters["@Distance"].Value = item.Distance;
            command.Parameters["@Name"].Value = item.Name;
            command.Parameters["@Date"].Value = item.Date;
            command.Parameters["@Time"].Value = item.Time;
            command.Parameters["@NationalityCode"].Value = item.NationalityCode;
        }
    }
}