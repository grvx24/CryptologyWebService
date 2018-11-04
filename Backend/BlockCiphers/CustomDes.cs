using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.BlockCiphers
{
    public class CustomDes : BlockCiphers
    {
    private DESCryptoServiceProvider des;

    public CustomDes(bool padding = true)
    {
        this.des = new DESCryptoServiceProvider();
        this.Padding = padding;
    }

    public override byte[] GenerateKey(int length)
    {
        des.KeySize = length;
        des.GenerateKey();
        return des.Key;
    }

    public override byte[] GenerateIV()
    {
        des.GenerateIV();
        return des.IV;
    }

    public override byte[] Encrypt(byte[] message, byte[] key, byte[] IV)
    {
        des.KeySize = key.Length * 8;
        des.Key = key;
        SetCipherMode();


        if (des.Mode != System.Security.Cryptography.CipherMode.ECB)
            des.IV = IV;

        var encryptor = des.CreateEncryptor(des.Key, des.IV);
        byte[] encrypted;

        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(message);
            }

            encrypted = ms.ToArray();
        }

        des.Dispose();

        return encrypted;
    }

    public override byte[] Decrypt(byte[] message, byte[] key, byte[] IV)
    {
        byte[] decrypted = new byte[message.Length];
        int decryptedLength = 0;
        SetCipherMode();

        if (!this.Padding)
        {
            des.Padding = PaddingMode.None;
        }
        des.KeySize = key.Length * 8;
        des.Key = key;

        if (des.Mode != System.Security.Cryptography.CipherMode.ECB)
            des.IV = IV;

        var decryptor = des.CreateDecryptor();

        using (MemoryStream ms = new MemoryStream(message))
        {
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                decryptedLength = cs.Read(decrypted);
            }
        }
        byte[] result = new byte[decryptedLength];
        Array.Copy(decrypted, result, decryptedLength);
        des.Dispose();

        return result;
    }

    protected override void SetCipherMode()
    {
        switch (CipherMode)
        {
            case BlockCipherMode.CBC:
                des.Mode = System.Security.Cryptography.CipherMode.CBC;
                break;
            case BlockCipherMode.ECB:
                des.Mode = System.Security.Cryptography.CipherMode.ECB;
                break;
        }
    }
}
}
