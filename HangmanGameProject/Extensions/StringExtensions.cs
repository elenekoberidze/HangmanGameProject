using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameProject.Extensions
{
    public static class StringExtensions
    {
        public static string HideLetters(this string word,IEnumerable<char> guessed)
        {
            var sb = new StringBuilder();
            foreach(char c in word)
            {
                if (guessed.Contains(c)) sb.Append(c);
                else sb.Append('_');

            }
            return sb.ToString();
        }
        public static bool IsLetter(this char c)
        {
            return char.IsLetter(c);
        }
        public static int DistinctCount<T>(this IEnumerable<T> items)
        {
            return items.Distinct().Count();
        }
    }
}
