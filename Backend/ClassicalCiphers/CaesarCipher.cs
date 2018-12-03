using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CryptoWebService.Helpers;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class CaesarCipher : IClassicalCiphers
    {
        private Dictionary<char, int> _positionDictionary;
        private string _alphabet;

        public string Alphabet
        {
            get => _alphabet;
            set
            {
                _alphabet = value;
                _positionDictionary = new Dictionary<char, int>();

                for (var i = 0; i < _alphabet.Length; i++)
                {
                    _positionDictionary.Add(_alphabet[i], i);
                }
            }
        }

        public int Key{ get; set; }

        public CaesarCipher(int key)
        {
            this.Key = key;
        }

        public string Encrypt(string message)
        {
            if (Key > Alphabet.Length)
            {
                Key = Key % Alphabet.Length;
            }

            try
            {
                message = StringHelper.ReplaceWhitespace(message, "");
                message = message.ToUpper();

                var encrypted = new char[message.Length];

                for (var i = 0; i < message.Length; i++)
                {
                    var position = _positionDictionary[message[i]];
                    position += Key;
                    if (position >= Alphabet.Length)
                    {
                        position = position - Alphabet.Length;
                    }

                    encrypted[i] = Alphabet[position];
                }

                return new string(encrypted);
            }
            catch (Exception )
            {
                throw new Exception("Niedozwolony znak!");
            }

        }

        public string Decrypt(string message)
        {
            if (Key > Alphabet.Length)
            {
                Key = Key % Alphabet.Length;
            }
            try
            {
                message = StringHelper.ReplaceWhitespace(message, "");
                message = message.ToUpper();

                var decrypted = new char[message.Length]; 

                for (var i = 0; i < message.Length; i++)
                {
                    var position = _positionDictionary[message[i]];
                    position -= Key;
                    if (position < 0)
                    {
                        position = position + Alphabet.Length;
                    }

                    decrypted[i] = Alphabet[position];

                }

                return new string(decrypted);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }



    }
}
