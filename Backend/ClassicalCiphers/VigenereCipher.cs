using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class VigenereCipher : IClassicalCiphers
    {
        private Dictionary<char, int> _positionDictionary;
        public string Alphabet { get; set; }
        public string Key { get; set; }

        public VigenereCipher(string alphabet,string key)
        {
            this.Key = key;
            if (key.Length < 1)
            {
                throw new ArgumentException("Invalid key length!");
            }

            this.Alphabet = alphabet;
            _positionDictionary = new Dictionary<char, int>();

            for (int i = 0; i < Alphabet.Length; i++)
            {
                _positionDictionary.Add(Alphabet[i], i);
            }
        }

        public string Encrypt(string message)
        {
            try
            {
                message = message.ToUpper();

                var encrypted = new char[message.Length];

                int keyIndex = 0;
                for (int i = 0; i < message.Length; i++)
                {
                    int msgCharPosition = Alphabet[message[i]];
                    int keyCharPosition = _positionDictionary[Key[keyIndex]];
                    int newPosition = (msgCharPosition + keyCharPosition) % (Alphabet.Length);

                    encrypted[i] = Alphabet[newPosition];
                    
                    keyIndex++;
                    if (keyIndex >= Key.Length)
                    {
                        keyIndex = 0;
                    }
                }

                return new string(encrypted);
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

                var decrypted = new char[message.Length];

                char[] inversedKey = new char[Key.Length]; 
                for (int i = 0; i < Key.Length; i++)
                {
                    var position = (Alphabet.Length - _positionDictionary[Key[i]])%Alphabet.Length;
                    inversedKey[i] = Alphabet[position];
                }

                int keyIndex = 0;
                for (int i = 0; i < message.Length; i++)
                {
                    int msgCharPosition = Alphabet[message[i]];
                    int keyCharPosition = _positionDictionary[inversedKey[keyIndex]];
                    int newPosition = (msgCharPosition + keyCharPosition) % (Alphabet.Length);

                    decrypted[i] = Alphabet[newPosition];

                    keyIndex++;
                    if (keyIndex >= Key.Length)
                    {
                        keyIndex = 0;
                    }
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
