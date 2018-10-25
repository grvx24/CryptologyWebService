using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoWebService.Helpers;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class BaconCipher : IClassicalCiphers
    {
        private readonly Dictionary<char, string> _charDictionary;
        private readonly Dictionary<string, char> _stringDictionary;
        private readonly string _alphabet = Alphabets.ALPHABET_EN_WITHOUT_J_V;
        private static readonly string[] Keys =
        {
            "AAAAA","AAAAB","AAABA","AAABB","AABAA",
            "AABAB","AABBA","AABBB","ABAAA","ABAAB",
            "ABABA","ABABB","ABBAA","ABBAB","ABBBA",
            "ABBBB","BAAAA","BAAAB","BAABA","BAABB",
            "BABAA","BABAB","BABBA","BABBB"
        };



        public BaconCipher()
        {
            _charDictionary=new Dictionary<char, string>();
            _stringDictionary = new Dictionary<string, char>();

            var keysIndex = 0;
            for (int i = 0; i < _alphabet.Length; i++)
            {
                _charDictionary.Add(_alphabet[i], Keys[keysIndex]);
                keysIndex++;
            }

            foreach (var item in _charDictionary)
            {
                _stringDictionary.Add(item.Value,item.Key);
            }


        }

        public string Encrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();
            try
            {
                var encrypted = new StringBuilder();

                for (int i = 0; i < message.Length; i++)
                {
                    encrypted.Append(_charDictionary[message[i]]);
                }

                return encrypted.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public string Decrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();

            try
            {
                message = message.ToUpper();
                var decrypted = new StringBuilder();

                var stringParts=StringHelper.SplitInParts(message, 5);

                foreach (var stringPart in stringParts)
                {
                    decrypted.Append(_stringDictionary[stringPart]);
                }

                return decrypted.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
