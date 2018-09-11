using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class BaconCipher : IClassicalCiphers
    {
        private Dictionary<char, string> _charDictionary;
        private Dictionary<string, char> _stringDictionary;
        private static readonly string[] keys =
        {
            "aaaaa","aaaab","aaaba","aaabb","aabaa",
            "aabab","aabba","aabbb","abaaa","abaab",
            "ababa","ababb","abbaa","abbab","abbba",
            "abbbb","baaaa","baaab","baaba","baabb",
            "babaa","babab","babba","babbb"
        };

        public static IEnumerable<String> SplitInParts(string s, int partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public BaconCipher()
        {
            _charDictionary=new Dictionary<char, string>();
            _stringDictionary = new Dictionary<string, char>();

            var keysIndex = 0;
            for (int i = 0; i < Alphabets.ALPHABET_EN.Length; i++)
            {
                if (Alphabets.ALPHABET_EN[i] == 'I'|| Alphabets.ALPHABET_EN[i] == 'U')
                {
                    _charDictionary.Add(Alphabets.ALPHABET_EN[i], keys[keysIndex]);
                    
                }
                else
                {
                    _charDictionary.Add(Alphabets.ALPHABET_EN[i], keys[keysIndex]);
                    keysIndex++;
                }
            }

            foreach (var item in _charDictionary)
            {
                _stringDictionary.Add(item.Value,item.Key);
            }


        }

        public string Encrypt(string message)
        {
            try
            {
                message = message.ToUpper();
                var encrypted = new StringBuilder();

                for (int i = 0; i < message.Length; i++)
                {
                    encrypted.Append(_charDictionary[message[i]]);
                }

                return encrypted.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        public string Decrypt(string message)
        {
            try
            {
                message = message.ToUpper();
                var decrypted = new StringBuilder();

                var stringParts=SplitInParts(message, 5);

                foreach (var stringPart in stringParts)
                {
                    decrypted.Append(_stringDictionary[stringPart]);
                }

                return decrypted.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
