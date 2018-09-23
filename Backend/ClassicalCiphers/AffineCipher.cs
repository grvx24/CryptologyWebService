using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using CryptoWebService.Helpers;


namespace CryptoWebService.Backend.ClassicalCiphers
{
    public class AffineCipher : IClassicalCiphers
    {
        private Dictionary<char, int> _positionDictionary;
        public string Alphabet { get; set; }

        public int A { get; set; }
        public int B { get; set; }

        public AffineCipher(int A,int B)
        {
            if (A % 2 == 0)
            {
                throw new ArgumentException("Argument A must be odd",nameof(A));
            }

            this.A = A;
            this.B = B;

            _positionDictionary = new Dictionary<char, int>();

            for (var i = 0; i < Alphabet.Length; i++)
            {
                _positionDictionary.Add(Alphabet[i], i);
            }
        }

        public string Encrypt(string message)
        {
            try
            {
                char[] encrypted = new char[message.Length];
                for (int i = 0; i < message.Length; i++)
                {
                    int offset = (A * _positionDictionary[message[i]] + B) % Alphabet.Length;
                    encrypted[i] = Alphabet[offset];
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
            var aInversed = MathHelper.ModInverse(A, Alphabet.Length);
            char[] decrypted = new char[message.Length];
            try
            {
                for (int i = 0; i < message.Length; i++)
                {
                    var charPosition = _positionDictionary[message[i]];
                    var offset = (aInversed * (charPosition - B)) % Alphabet.Length;
                    decrypted[i] = Alphabet[offset];
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
