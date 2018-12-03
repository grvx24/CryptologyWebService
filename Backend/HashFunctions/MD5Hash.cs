using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions
{
    public class MD5Hash : IHashFunctions
    {
       
        public string Encrypt(string message)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            try
            { 
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(message));
 
                byte[] result = md5.Hash;

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

