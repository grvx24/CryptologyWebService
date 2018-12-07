using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class Alphabets
    {
        public static readonly string ALPHABET_EN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly string ALPHABET_EN_WITHOUT_J_V = "ABCDEFGHIKLMNOPQRSTUWXYZ";
        public static readonly string ALPHABET_EN_WITHOUT_J = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        public static readonly string ALPHABET_PL = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUWYZŹŻ";
        public static readonly string DIGITS = "0123456789";
        public static readonly string PUNCTUATION = "!\"#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~";
        public static readonly string WHITESPACE = " \t\n";
        public static readonly string ALPHABET_EN_DIGITS = ALPHABET_EN + DIGITS;
        public static readonly string ALPHABET_PL_DIGITS = ALPHABET_PL + DIGITS;
        public static readonly string ALPHABET_EN_EXTENDED = ALPHABET_EN + DIGITS+ PUNCTUATION;
        public static readonly string ALPHABET_PL_EXTENDED = ALPHABET_PL + DIGITS+ PUNCTUATION;

        public static readonly string[] AVAILABLE_ALPHABETS_NAME =
        {
            Text.Alphabet_EN,
            Text.Alphabet_PL,
            Text.Alphabet_EN_Digit,
            Text.Alphabet_PL_Digit,
            Text.Alphabet_EN_Extended,
            Text.Alphabet_PL_Extended
        };
        public enum AlphabetType
        {
            EN,
            PL,
            EN_Digits,
            PL_Digits,
            EN_Digits_Extended,
            PL_Digits_Extended
        }

        public static string GetAlphabet(AlphabetType type)
        {
            switch (type)
            {
                case AlphabetType.EN:
                    return ALPHABET_EN;
                case AlphabetType.PL:
                    return ALPHABET_PL;
                case AlphabetType.EN_Digits:
                    return ALPHABET_EN + DIGITS;
                case AlphabetType.PL_Digits:
                    return ALPHABET_PL + DIGITS;
                case AlphabetType.EN_Digits_Extended:
                    return ALPHABET_EN + DIGITS + PUNCTUATION;
                case AlphabetType.PL_Digits_Extended:
                    return ALPHABET_PL + DIGITS + PUNCTUATION;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

    }
}
