using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.BlockCiphers
{
    public enum BlockCipherMode
    {
        CBC,
        ECB
    }

    public abstract class BlockCiphers
    {
        public BlockCipherMode CipherMode { get; set; }
        public bool Padding { get; set; }
        public abstract byte[] GenerateKey(int length);
        public abstract byte[] GenerateIV();
        public abstract byte[] Encrypt(byte[] message, byte[] key, byte[] IV);
        public abstract byte[] Decrypt(byte[] message, byte[] key, byte[] IV);
        protected abstract void SetCipherMode();

    }

}
