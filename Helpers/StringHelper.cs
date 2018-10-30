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


        #region HexToByteConversion

        //BYTES --> HEX
        private static readonly uint[] Lookup32 = CreateLookup32();

        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }

        private static string ByteArrayToHexViaLookup32(byte[] bytes)
        {
            var lookup32 = Lookup32;
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }

        private static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            //return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        //HEX --> BYTES
        public static byte[] StringHexToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }



        #endregion

        public static byte[] BinaryStringToBytes(string input)
        {
            if (input.Length % 8 != 0)
            {
                int padding = 8 - input.Length % 8;
                char[] zeros = new char[padding];
                for (int i = 0; i < zeros.Length; i++)
                {
                    zeros[i] = '0';
                }
                string begin = new string(zeros);

                input = string.Join(begin, input);
            }

            int numOfBytes = input.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            }

            return bytes;
        }


    }
}
