using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveDiacritics(this string text)
        {
            var result = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(letter);
                }
            }
            return result.ToString();
        }

        public static bool ManualContains(this string str, string contained)
        {

            var i = 0;
            var j = contained.Count();
            while (j <= str.Count())
            {
                var substr = str.Slice(i, j);
                if (substr == contained)
                {
                    return true;
                }

                i++;
                j++;
            }

            return false;
        }

        public static string Slice(this string source, int start, int end)
        {
            if (end < 0) // Keep this for negative end support
            {
                end = source.Length + end;
            }
            int len = end - start;               // Calculate length
            return source.Substring(start, len); // Return Substring of length
        }
    }
}
