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
        private readonly string _alphabet = Alphabets.ALPHABET_EN;
        private static readonly string[] Keys =
        {
            "aaaaa","aaaab","aaaba","aaabb","aabaa",
            "aabab","aabba","aabbb","abaaa","abaab",
            "ababa","ababb","abbaa","abbab","abbba",
            "abbbb","baaaa","baaab","baaba","baabb",
            "babaa","babab","babba","babbb"
        };



        public BaconCipher()
        {
            _charDictionary=new Dictionary<char, string>();
            _stringDictionary = new Dictionary<string, char>();

            var keysIndex = 0;
            for (int i = 0; i < _alphabet.Length; i++)
            {
                if (_alphabet[i] == 'I'|| _alphabet[i] == 'U')
                {
                    _charDictionary.Add(_alphabet[i], Keys[keysIndex]);
                    
                }
                else
                {
                    _charDictionary.Add(_alphabet[i], Keys[keysIndex]);
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

                var stringParts=StringHelper.SplitInParts(message, 5);

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
