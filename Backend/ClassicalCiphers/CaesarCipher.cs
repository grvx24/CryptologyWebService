using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class CaesarCipher : IClassicalCiphers
    {
        private Dictionary<char, int> _positionDictionary;
        public string Alphabet { get; set; }
        public int Offset{ get; set; }

        public CaesarCipher(string alphabet,int offset)
        {
            this.Alphabet = alphabet;
            this.Offset = offset;
            _positionDictionary = new Dictionary<char, int>();

            for (var i = 0; i < alphabet.Length; i++)
            {
                _positionDictionary.Add(alphabet[i], i);
            }
        }

        public string Encrypt(string message)
        {
            try
            {
                message = message.ToUpper();

                var encrypted = new char[message.Length];

                for (var i = 0; i < message.Length; i++)
                {
                    var position = _positionDictionary[message[i]];
                    position += Offset;
                    if (position >= Alphabet.Length)
                    {
                        position = position - Alphabet.Length;
                    }

                    encrypted[i] = Alphabet[position];
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

                for (var i = 0; i < message.Length; i++)
                {
                    var position = _positionDictionary[message[i]];
                    position -= Offset;
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
