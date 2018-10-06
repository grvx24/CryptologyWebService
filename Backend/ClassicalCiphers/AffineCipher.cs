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
        private string _alphabet;

        public string Alphabet
        {
            get => _alphabet;
            set
            {
                _alphabet = value;
                _positionDictionary = new Dictionary<char, int>();

                for (var i = 0; i < Alphabet.Length; i++)
                {
                    _positionDictionary.Add(Alphabet[i], i);
                }
            }
        }

        public int A { get; set; }
        public int B { get; set; }

        public AffineCipher(int a,int b)
        {
            if (a % 2 == 0)
            {
                throw new ArgumentException("Argument A must be odd",nameof(a));
            }

            this.A = a;
            this.B = b;


        }

        public string Encrypt(string message)
        {
            message = StringHelper.ReplaceWhitespace(message, "");
            message = message.ToUpper();
            char[] encrypted = new char[message.Length];
            try
            {
                for (int i = 0; i < message.Length; i++)
                {
                    int offset = (A * _positionDictionary[message[i]] + B) % Alphabet.Length;
                    encrypted[i] = Alphabet[offset];
                }

                return new string(encrypted);
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

            var aInversed = MathHelper.ModInverse(A, Alphabet.Length);
            char[] decrypted = new char[message.Length];
            try
            {
                for (int i = 0; i < message.Length; i++)
                {
                    var charPosition = _positionDictionary[message[i]];
                    var x = (aInversed * (charPosition - B));
                    var offset = MathHelper.Modulo(x, Alphabet.Length);
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
