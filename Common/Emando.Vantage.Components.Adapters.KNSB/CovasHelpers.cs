using System.Globalization;
using System.Linq;
using System.Text;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    public static class CovasHelpers
    {
        public static string RemoveDiacritics(this string s)
        {
            if (s == null)
                return null;

            var normalizedString = s.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in from c in normalizedString
                              let unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c)
                              where unicodeCategory != UnicodeCategory.NonSpacingMark
                              select c)
                stringBuilder.Append(c);

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}