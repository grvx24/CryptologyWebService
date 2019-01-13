using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CryptoWebService.Backend.HashFunctions.Keccak
{
    public sealed class Sha3HashAlgorithm : HashAlgorithm
    {
        public enum Size : byte
        {
            Bits224,
            Bits256,
            Bits384,
            Bits512
        }

        private readonly Sha3Permutation _permutation;

        private Bitstring _hash = new Bitstring();

        public Sha3HashAlgorithm(Size size)
            : base()
        {
            switch (size)
            {
                case Size.Bits224:
                    _permutation = Sha3Permutation.Sha3_224();
                    break;
                case Size.Bits256:
                    _permutation = Sha3Permutation.Sha3_256();
                    break;
                case Size.Bits384:
                    _permutation = Sha3Permutation.Sha3_384();
                    break;
                case Size.Bits512:
                default:
                    _permutation = Sha3Permutation.Sha3_512();
                    break;
            }
        }

        public static Sha3HashAlgorithm Create(Size size)
        {
            return new Sha3HashAlgorithm(size);
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            byte[] data = new byte[cbSize];
            Array.Copy(array, ibStart, data, 0, cbSize);
            _hash.Append(data);
        }

        protected override byte[] HashFinal()
        {
            _hash = new Bitstring(_permutation.Process(_hash.Bytes, _permutation.Width));
            return _hash?.Bytes ?? new byte[0];
        }

        public override void Initialize()
        {
            _hash = new Bitstring();
        }
    }
}
