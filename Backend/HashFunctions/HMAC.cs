using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions
{

    public class HMAC : IHMAC
    { 

    public enum HashFunctionType
    {
        MD5,
        SHA1,
        SHA256,
        SHA512
    }
        public HashFunctionType HashType { get; set; }

        public HMAC(HashFunctionType type)
        {
            HashType = type;
        }

        public string Encrypt(string message, string key)
        {
            byte[] result=null;
            StringBuilder hash = new StringBuilder();

            switch (HashType)
            {
                case HashFunctionType.MD5:
                    HMACMD5 hmd5 = new HMACMD5(ASCIIEncoding.ASCII.GetBytes(key));
                    hmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(message));  
                    result = hmd5.Hash;
                    break;

                case HashFunctionType.SHA1:
                    HMACSHA1 hsha1 = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(key));  
                    hsha1.ComputeHash(ASCIIEncoding.ASCII.GetBytes(message)); 
                    result = hsha1.Hash;
                    break;

                case HashFunctionType.SHA256:
                    HMACSHA256 hsha256 = new HMACSHA256(ASCIIEncoding.ASCII.GetBytes(key));
                    hsha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(message));  
                    result = hsha256.Hash;
                    break;

                case HashFunctionType.SHA512:
                    HMACSHA512 hsha512 = new HMACSHA512(ASCIIEncoding.ASCII.GetBytes(key));
                    hsha512.ComputeHash(ASCIIEncoding.ASCII.GetBytes(message));
                    result = hsha512.Hash;         
                    break;
            }

            for (int i = 0; i < result.Length; i++)
            {
                hash.Append(result[i].ToString("x2"));
            }

            return hash.ToString();
        }

    }
}
