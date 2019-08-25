using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    internal static class LongTrackLicenses
    {
        public const string Discipline = "SpeedSkating.LongTrack";
        public const string IssuerId = "KNSB";

        private static readonly IDictionary<int, string> Categories = new Dictionary<int, string>
        {
            { 7, "PF" },
            { 8, "PE" },
            { 9, "PD" },
            { 10, "PC" },
            { 11, "PB" },
            { 12, "PA" },
            { 13, "C1" },
            { 14, "C2" },
            { 15, "B1" },
            { 16, "B2" },
            { 17, "A1" },
            { 18, "A2" },
            { 19, "N1" },
            { 20, "N2" },
            { 21, "N3" },
            { 22, "N4" },
            { 29, "SA" },
            { 38, "SB" },
            { 43, "40" },
            { 48, "45" },
            { 53, "50" },
            { 58, "55" },
            { 63, "60" },
            { 68, "65" },
            { int.MaxValue, "70" }
        };

        public static DateTime ValidFrom(DateTime date)
        {
            int year = date.Month >= 7 ? date.Year : date.Year - 1;
            return new DateTime(year, 7, 1);
        }

        public static DateTime ValidTo(DateTime date)
        {
            int year = date.Month < 7 ? date.Year : date.Year + 1;
            return new DateTime(year, 7, 1);
        }

        private static int Age(DateTime birthDate, DateTime referenceDate)
        {
            int age = referenceDate.Year - birthDate.Year;
            if (birthDate > referenceDate.AddYears(-age))
                age--;
            return age;
        }

        public static string Category(Gender gender, DateTime birthDate, DateTime date)
        {
            int age = Age(birthDate, ValidFrom(date));
            string code = (from c in Categories
                           where age <= c.Key
                           select c.Value).FirstOrDefault();
            if (code == null)
                return null;

            return (gender == Gender.Male ? 'H' : 'D') + code;
        }
    }
}