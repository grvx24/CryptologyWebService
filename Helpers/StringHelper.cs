using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoWebService.Helpers
{
    /// <summary>
    /// Contains string manipulation methods.
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// Splits the string into smaller parts.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="partLength">Length of the part.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Text</exception>
        /// <exception cref="ArgumentException">Part length has to be positive. - partLength</exception>
        public static IEnumerable<string> SplitInParts(string text, int partLength)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < text.Length; i += partLength)
                yield return text.Substring(i, Math.Min(partLength, text.Length - i));
        }



        private static readonly Regex WhitespaceRegex = new Regex(@"\s+");

        /// <summary>
        /// Replaces the whitespace.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns></returns>
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return WhitespaceRegex.Replace(input, replacement);
        }

    }
}
