using System;

namespace Emando.Vantage
{
    public enum Gender
    {
        Male,
        Female
    }

    public static class GenderExtensions
    {
        public static string ToLetter(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "M";
                case Gender.Female:
                    return "F";
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender));
            }
        }
    }
}