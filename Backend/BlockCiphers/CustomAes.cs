using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CryptoWebService.Backend.BlockCiphers
{
    public class CustomAes : BlockCiphers
    {
        private AesCryptoServiceProvider aes;

        public CustomAes(bool padding=true)
        {
            this.aes = new AesCryptoServiceProvider();
            this.Padding = padding;
        }

        public override byte[] GenerateKey(int length)
        {
            aes.KeySize = length;
            aes.GenerateKey();
            return aes.Key;
        }

        public override byte[] GenerateIV()
        {
            aes.GenerateIV();
            return aes.IV;
        }

        public override byte[] Encrypt(byte[] message, byte[] key, byte[] IV)
        {
            aes.KeySize = key.Length*8;
            aes.Key = key;
            SetCipherMode();


            if (aes.Mode!=System.Security.Cryptography.CipherMode.ECB)
                aes.IV = IV;

            var encryptor = aes.CreateEncryptor(aes.Key,aes.IV);
            byte[] encrypted;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms,encryptor,CryptoStreamMode.Write) )
                {
                    cs.Write(message);
                }

                encrypted = ms.ToArray();
            }

            aes.Dispose();

            return encrypted;
        }

        public override byte[] Decrypt(byte[] message, byte[] key, byte[] IV)
        {
            byte[] decrypted = new byte[message.Length];
            int decryptedLength = 0;
            SetCipherMode();

            if (!this.Padding)
            {
                aes.Padding = PaddingMode.None;
            }
            aes.KeySize = key.Length*8;
            aes.Key = key;

            if (aes.Mode != System.Security.Cryptography.CipherMode.ECB)
                aes.IV = IV;

            var decryptor = aes.CreateDecryptor();

            using (MemoryStream ms = new MemoryStream(message))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    decryptedLength = cs.Read(decrypted);
                }
            }
            byte[] result = new byte[decryptedLength];
            Array.Copy(decrypted, result, decryptedLength);
            aes.Dispose();

            return result;
        }

        protected override void SetCipherMode()
        {
            switch (CipherMode)
            {
                case BlockCipherMode.CBC:
                    aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                    break;
                case BlockCipherMode.ECB:
                    aes.Mode = System.Security.Cryptography.CipherMode.ECB;
                    break;
            }
        }
    }
}
