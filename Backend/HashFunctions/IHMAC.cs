using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoWebService.Backend.HashFunctions
{

    interface IHMAC
    {
        string Encrypt(string message,string key);
    }
}
