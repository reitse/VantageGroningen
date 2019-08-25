using System.Globalization;

namespace Emando.Vantage.Api.Models
{
    public static class StringExtensions
    {
        public static string ToInvariantTitleCase(this string s)
        {
            return s != null ? CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower()) : null;
        }
    }
}