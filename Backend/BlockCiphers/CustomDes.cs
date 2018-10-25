using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.BlockCiphers
{
    public class CustomDes : BlockCiphers
    {
        public override byte[] GenerateKey(int length)
        {
            throw new NotImplementedException();
        }

        public override byte[] GenerateIV()
        {
            throw new NotImplementedException();
        }

        public override byte[] Encrypt(byte[] message, byte[] key, byte[] IV)
        {
            throw new NotImplementedException();
        }

        public override byte[] Decrypt(byte[] message, byte[] key, byte[] IV)
        {
            throw new NotImplementedException();
        }

        protected override void SetCipherMode()
        {
            throw new NotImplementedException();
        }
    }
}
