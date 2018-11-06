using System;
using System.Linq;
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

        public static bool Match(this string original, string compareTo, params char[] splitters)
        {
            var splitted = original.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
            splitted.ToList().ForEach(x => x = x.Trim());
            return splitted.Any(x => x.Equals(compareTo, StringComparison.InvariantCultureIgnoreCase));
        }

    }
}
