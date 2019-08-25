using System;

namespace Emando.Vantage.Components
{
    public static class DateTimeExtensions
    {
        public static int Age(this DateTime birthDate)
        {
            return Age(birthDate, DateTime.Today);
        }

        public static int Age(this DateTime birthDate, DateTime reference)
        {
            var age = reference.Year - birthDate.Year;
            if (birthDate > reference.AddYears(-age))
                age--;
            return age;
        }
    }
}