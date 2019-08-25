using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Telerik.Reporting.Expressions;
using System.Collections.Generic;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public static class Functions
    {
        [Function(Category = "Vantage", Namespace = "Global")]
        public static DateTime? InTimeZone(DateTime? input, string timeZoneId)
        {
            if (timeZoneId == null || input == null)
                return input;

            TimeZoneInfo timeZone;
            try
            {
                timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                return input;
            }
            catch (InvalidTimeZoneException)
            {
                return input;
            }

            if (input.Value.Kind == DateTimeKind.Local)
                input = input.Value.ToUniversalTime();

            return TimeZoneInfo.ConvertTimeFromUtc(input.Value, timeZone);
        }

        [Function(Category = "Vantage", Namespace = "Global")]
        public static Bitmap DecodeImage(string base64)
        {
            if (base64 == null)
                return null;

            var buffer = Convert.FromBase64String(base64);
            using (var stream = new MemoryStream(buffer))
                return new Bitmap(stream);
        }

        [Function(Category = "Vantage", Namespace = "SpeedSkating.LongTrack")]
        public static string FormatRaceTime(Race race, long digits)
        {
            if (race?.PresentedResult == null)
                return null;

            if (race.PresentedResult.TimeInvalidReason != null)
                return FormatTimeInvalidReason(race.PresentedResult.TimeInvalidReason.Value);

            return race.PresentedTime != null ? FormatTime(race.PresentedTime.Time, digits) : null;
        }
        
        public static string FormatTimeInvalidReason(TimeInvalidReason timeInvalidReason)
        {
            return Resources.ResourceManager.GetString($"TimeInvalidReason_{(int)timeInvalidReason}");
        }

        [Function(Category = "Vantage", Namespace = "SpeedSkating.LongTrack")]
        public static string FormatLane(int lane)
        {
            return Resources.ResourceManager.GetString($"Lane_{lane}");
        }

        [Function(Category = "Vantage", Namespace = "SpeedSkating.LongTrack")]
        public static string FormatTimeInfo(Race race)
        {
            if (race?.PresentedTime == null)
                return null;

            return string.Join(" ", race.PresentedTime.TimeInfo.Expand().PresentationOrder().Select(j => Resources.ResourceManager.GetString($"TimeInfo_{(int)j}")));
        }

        [Function(Category = "Vantage", Namespace = "SpeedSkating.LongTrack")]
        public static string FormatTime(TimeSpan? time, long digits)
        {
            if (time == null)
                return null;

            var fraction = new string('f', (int)digits);
            string format;
            if (time.Value.TotalMinutes >= 1)
                format = "m\\:ss\\." + fraction;
            else
                format = "s\\." + fraction;

            return time.Value.ToString(format, CultureInfo.GetCultureInfo("en-US"));
        }

        [Function(Category = "Vantage", Namespace = "SpeedSkating.LongTrack")]
        public static string FormatTimeDifference(TimeSpan? time, long digits)
        {
            if (time == null)
                return null;

            var fraction = new string('f', (int)digits);
            string format;
            if (time.Value.TotalMinutes >= 1)
                format = "m\\:ss\\." + fraction;
            else
                format = "s\\." + fraction;

            var sign = time < TimeSpan.Zero ? "-" : "+";
            return sign + time.Value.ToString(format, CultureInfo.GetCultureInfo("en-US"));
        }

        [Function(Category = "Vantage", Namespace = "Global")]
        public static string FormatPeriod(DateTime? starts, DateTime? ends)
        {
            if (!starts.HasValue)
                return null;

            if (!ends.HasValue)
                ends = starts.Value;

            var daysInBetween = (ends.Value.Date - starts.Value.Date).Days;
            if (daysInBetween == 0)
                return starts.Value.ToString("d MMMM yyyy");

            string startsFormatted;
            if (starts.Value.Month == ends.Value.Month && starts.Value.Year == ends.Value.Year)
                startsFormatted = starts.Value.Day.ToString();
            else if (starts.Value.Year == ends.Value.Year)
                startsFormatted = starts.Value.ToString("d MMMM");
            else
                startsFormatted = starts.Value.ToString("d MMMM yyyy");

            var periodFormat = daysInBetween == 1 ? Resources.Dates_TwoDays : Resources.Dates_MultipleDays;
            return string.Format(periodFormat, startsFormatted, ends.Value.ToString("d MMMM yyyy"));
        }

        [Function(Category = "Vantage", Namespace = "Global")]
        public static string FormatMembers(TeamCompetitor competitor)
        {
            if (competitor == null)
                return null;

            var formatter = new Func<TeamCompetitorMember, string>(m => m.Reserve.HasValue
                ? string.Format(Resources.ReserveFormat, m.Member.FullName, m.Reserve.Value)
                : m.Member.FullName);

            return string.Join(", ", competitor.Members.OrderBy(m => m.Order).Select(m => formatter(m)));
        }

        [Function(Category = "Vantage", Namespace = "Global")]
        public static decimal? PointsAtDistance(IDictionary<Race, decimal> races, int distance)
        {
            return races.Where(r => r.Key.Distance.Number == distance).Select(r => r.Value).Sum();
        }
    }
}