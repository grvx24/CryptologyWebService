using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions
{
    interface IHashFunctions
    {
        string Encrypt(string message);
        string Decrypt(string message);
    }
}
