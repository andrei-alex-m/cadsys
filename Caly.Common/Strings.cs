using System;
using System.Text;

namespace Caly.Common
{
    public static class StringsExtensions
    {
        public static bool ContainsAny(this string haystack, params string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                    return true;
            }

            return false;
        }

        public static string ReplaceMultiple(this string original, char with, params char[] replace)
        {
            string[] temp;

            temp = original.Split(replace, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(with, temp);
        }

    }
}
