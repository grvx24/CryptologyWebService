using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions
{
    public class SHA1Hash : IHashFunctions
    {

        public string Encrypt(string message)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            try
            {
                sha1.ComputeHash(ASCIIEncoding.ASCII.GetBytes(message));

                byte[] result = sha1.Hash;

                StringBuilder hash = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    hash.Append(result[i].ToString("x2"));
                }

                return hash.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}