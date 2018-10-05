using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class Alphabets
    {
        public static readonly string ALPHABET_EN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly string ALPHABET_PL = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUWYZŹŻ";
        public static readonly string DIGITS = "0123456789";
        public static readonly string PUNCTUATION = "!\"#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~";
        public static readonly string WHITESPACE = "\t\n\r\x0b\x0c";
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

        public string Alphabet { get; set; }

        public Alphabets(AlphabetType type)
        {
            switch (type)
            {
                case AlphabetType.EN:
                    Alphabet = ALPHABET_EN;
                    break;
                case AlphabetType.PL:
                    Alphabet = ALPHABET_PL;
                    break;
                case AlphabetType.EN_Digits:
                    Alphabet = ALPHABET_EN + DIGITS;
                    break;
                case AlphabetType.PL_Digits:
                    Alphabet = ALPHABET_PL + DIGITS;
                    break;
                case AlphabetType.EN_Digits_Extended:
                    Alphabet = ALPHABET_EN + DIGITS + PUNCTUATION+WHITESPACE;
                    break;
                case AlphabetType.PL_Digits_Extended:
                    Alphabet = ALPHABET_PL + DIGITS + PUNCTUATION + WHITESPACE;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

    }
}
