using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.AsymmetricCryptography
{
    public class BasicRsa
    {
        public BigInteger PHI { get; private set; }
        public BigInteger E { get; private set; }
        public BigInteger N { get; private set; }
        public BigInteger D { get; private set; }

        public BigInteger C { get; private set; }

        private readonly List<int> primes = PrimeNumbers.Primes;

        public static BigInteger GCD(BigInteger a, BigInteger b)
        {
            BigInteger c;
            while (b != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }
            return a;
        }

        public void GenerateKey(BigInteger p, BigInteger q)
        {
            N = p * q;
            PHI = (p - 1) * (q - 1);


            for (int i = 0; i < primes.Count; i++)
            {
                ulong item = (ulong)primes[i];
                if (GCD(item, PHI) == 1)
                {
                    E = item;
                    break;
                }
            }

            if (E == 0)
            {
                throw new ArgumentException("Za mało liczb pierwszych!");
            }

            for (int d = 2; d < int.MaxValue; d++)
            {
                if ((d * E) % PHI == 1)
                {
                    D = d;
                    break;
                }
            }
        }

        public BigInteger Encrypt(int m)
        {
            C = BigInteger.ModPow(m, E, N);

            return C;
        }

        public BigInteger Encrypt(int m, int e, BigInteger n)
        {
            var result = BigInteger.ModPow(m, e, n);

            return result;
        }
        public BigInteger Encrypt(int m, BigInteger e, BigInteger n)
        {
            var result = BigInteger.ModPow(m, e, n);

            return result;
        }

        public BigInteger Encrypt(BigInteger m)
        {
            C = BigInteger.ModPow(m, E, N);

            return C;
        }

        public BigInteger Decrypt(int c)
        {
            var m = BigInteger.ModPow(c, D, N);

            return m;
        }

        public BigInteger Decrypt(BigInteger c)
        {
            var m = BigInteger.ModPow(c, D, N);

            return m;
        }

        public int Decrypt(BigInteger c, int d, BigInteger n)
        {
            var m = BigInteger.ModPow(c, d, n);
            return (int)m;
        }

        public int Decrypt(BigInteger c, BigInteger d, BigInteger n)
        {
            var m = BigInteger.ModPow(c, d, n);
            return (int)m;
        }

    }
}
