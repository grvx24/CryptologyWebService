using SHA3.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions
{
    public class SHA3Hash : IHashFunctions
    {
        public enum HashFunctionSize
        {
            Bits224,
            Bits256,
            Bits384,
            Bits512
        }
        public HashFunctionSize HashSize { get; set; }

        public SHA3Hash(HashFunctionSize size)
        {
            HashSize = size;
        }
        public string Encrypt(string message)
        {
                byte[] result = null;
                StringBuilder hash = new StringBuilder();

                switch (HashSize)
                {
                    case HashFunctionSize.Bits224:
                            result = Sha3.Sha3224().ComputeHash(Encoding.UTF8.GetBytes(message));
                    break;

                case HashFunctionSize.Bits256:
                            result = Sha3.Sha3256().ComputeHash(Encoding.UTF8.GetBytes(message));
                    break;

                case HashFunctionSize.Bits384:
                            result = Sha3.Sha3384().ComputeHash(Encoding.UTF8.GetBytes(message));
                    break;

                 case HashFunctionSize.Bits512:
                            result = Sha3.Sha3512().ComputeHash(Encoding.UTF8.GetBytes(message));
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
